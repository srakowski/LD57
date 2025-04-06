using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace LastDitchEffort;

public class LastDitchEffortGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _placeholder;
    private GameBoard _gb;

    public LastDitchEffortGame()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1440,
        };

        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _gb = new GameBoard();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _placeholder = Content.Load<Texture2D>("placeholder");
    }

    private MouseState _prevM;
    private MouseState _currM;

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //_prevM = _currM;
        //_currM = Mouse.GetState();

        //if (_prevM.LeftButton == ButtonState.Pressed &&
        //    _currM.LeftButton == ButtonState.Released)
        //{
        //    _gb.SwapAt(_currM.Position);
        //}        

        HandleInput();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _gb.Draw(_spriteBatch, _placeholder);

        base.Draw(gameTime);
    }

    private KeyboardState _currState;
    private KeyboardState _prevState;

    private void HandleInput()
    {
        _prevState = _currState;
        _currState = Keyboard.GetState();

        if (KeyWasPressed(Keys.D) || KeyWasPressed(Keys.Right))
        {
            _gb.ActionRight();
        }

        if (KeyWasPressed(Keys.A) || KeyWasPressed(Keys.Left))
        {
            _gb.ActionLeft();
        }

        if (KeyWasPressed(Keys.W) || KeyWasPressed(Keys.Up))
        {
            _gb.ActionUp();
        }

        if (KeyWasPressed(Keys.S) || KeyWasPressed(Keys.Down))
        {
            _gb.ActionDown();
        }

        //if (KeyWasPressed(Keys.Space) || KeyWasPressed(Keys.RightControl))
        //{
        //    _player.SelectTool(PlayerTool.Mine);
        //}
    }

    private bool KeyWasPressed(Keys key)
    {
        return _prevState.IsKeyUp(key) && _currState.IsKeyDown(key);
    }
}

public class GameBoard
{
    public int Rows = 15;

    public int Columns = 5;

    private (int Row, int Col) _sorterPos;

    public Dictionary<(int Row, int Col), GameBoardSlot> _slots = [];

    public GameBoard()
    {
        var cutoffRow = Rows / 2;
        for (var col = 0; col < Columns; col++)
        {
            for (var row = 0; row < Rows; row++)
            {
                _slots[(row, col)] = new GameBoardSlot
                {
                    Content = row > cutoffRow
                        ? ContentSpawner.Spawn() 
                        : SlotContent.Empty
                };

                if (row == cutoffRow + 1 && col == 2)
                {
                    _sorterPos = (row, col);
                    _slots[_sorterPos].Content = SlotContent.Sorter;
                }
            }
        }
    }

    private static readonly (int Row, int Col)[] _offsets = new[]
    {
        (-1, 0),
        (0, -1),
        (0, 1),
        (1, 0),
    };

    //internal void SwapAt(Point screenPos)
    //{
    //    var bCol = screenPos.X / 40;
    //    var bRow = screenPos.Y / 40;

    //    if (!_slots.TryGetValue((bRow, bCol), out var slot))
    //    {
    //        return;
    //    }

    //    (int Row, int Col)? swapSpot = null;
    //    foreach (var offset in _offsets)
    //    {
    //        var (rd, cd) = offset;
    //        var sRow = bRow + rd;
    //        var sCol = bCol + cd;

    //        if (!_slots.TryGetValue((sRow, sCol), out var adjSlot) || adjSlot.Content != SlotContent.Sorter)
    //        {
    //            continue;
    //        }

    //        swapSpot = (sRow, sCol);
    //    }

    //    if (!swapSpot.HasValue)
    //    {
    //        return;
    //    }

    //    var sorter = _slots[swapSpot.Value];
    //    _slots[swapSpot.Value] = _slots[(bRow, bCol)];
    //    _slots[(bRow, bCol)] = sorter;
    //}

    public void Draw(SpriteBatch sb, Texture2D ph)
    {
        sb.Begin(blendState: BlendState.NonPremultiplied);

        for (var col = 0; col < Columns; col++)
        {
            for (var row = 0; row < Rows; row++)
            {
                var slot = _slots[(row, col)];
                if (slot.Content == SlotContent.Empty)
                {
                    continue;
                }

                var color = slot.Content switch
                {
                    SlotContent.Sorter => Color.Blue,
                    SlotContent.Rock => Color.WhiteSmoke,
                    SlotContent.Iron => Color.DarkRed,
                    SlotContent.Copper => Color.DarkOrange,
                    SlotContent.Coal => Color.Green,
                    SlotContent.Gold => Color.Gold,
                    _ => throw new NotImplementedException()
                };

                var pos = new Vector2(col, row) * new Vector2(80, 80);
                sb.Draw(ph, pos, color);
            }
        }

        sb.End();
    }

    internal void ActionUp()
    {
        var newSorterPos = (_sorterPos.Row - 1, _sorterPos.Col);
        TrySwap(newSorterPos);
    }

    internal void ActionDown()
    {
        var newSorterPos = (_sorterPos.Row + 1, _sorterPos.Col);
        TrySwap(newSorterPos);
    }

    internal void ActionLeft()
    {
        var newSorterPos = (_sorterPos.Row, _sorterPos.Col - 1);
        TrySwap(newSorterPos);
    }

    internal void ActionRight()
    {
        var newSorterPos = (_sorterPos.Row, _sorterPos.Col + 1);
        TrySwap(newSorterPos);
    }

    private void TrySwap((int Row, int Col) newSorterPos)
    {
        if (!_slots.TryGetValue(newSorterPos, out var rsx) ||
            rsx.Content == SlotContent.Empty)
        {
            return;
        }

        var sorter = _slots[_sorterPos];
        _slots[_sorterPos] = _slots[newSorterPos];
        _slots[newSorterPos] = sorter;
        _sorterPos = newSorterPos;
    }
}

public enum SlotContent
{
    Empty = 0,
    Sorter,
    Rock,
    Iron,
    Copper,
    Coal,
    Gold,
}

public class GameBoardSlot
{
    public SlotContent Content;
}

static class ContentSpawner
{
    public static Random rand = new Random();

    public static SlotContent Spawn()
    {
        var v = rand.Next(100);
        return v switch
        {
            > 40 => SlotContent.Rock,
            > 30 => SlotContent.Iron,
            > 20 => SlotContent.Copper,
            > 10 => SlotContent.Coal,
            _ => SlotContent.Gold, 
        };
    }
}