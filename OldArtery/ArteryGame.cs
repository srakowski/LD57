namespace Artery;

using Microsoft.Xna.Framework;
using System;

class ArteryGame : Game
{
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _sb;

    private Conwin _testConwin;
    private Region _region;
    private Player _player;

    public ArteryGame()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1440,
        };

        _graphics.ApplyChanges();

        Window.AllowUserResizing = true;

        Content.RootDirectory = "Content";

        _testConwin = new Conwin(100, 100);

        _testConwin.Write("Hello World. Press [e]");
        _region = new Region(new Rectangle(0, 0, 100, 100));
        _player = new Player(10, 10);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        _sb = new SpriteBatch(GraphicsDevice);
        Font.Texture = Content.Load<Texture2D>("font");
        Region.CellTexture = Content.Load<Texture2D>("Sprites/cell");
        Player.Texture = Content.Load<Texture2D>("Sprites/player");
        Resources.Texture = Content.Load<Texture2D>("Sprites/resources");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.F11))
            _graphics.ToggleFullScreen();

        base.Update(gameTime);

        HandleInput();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var m = Matrix.Identity * Matrix.CreateScale(3f);
        _sb.Begin(transformMatrix: m, samplerState: SamplerState.PointWrap);
        _region.Draw(_sb);
        _player.Draw(_sb);
        _sb.End();

        //var m = Matrix.Identity * Matrix.CreateScale(1f);
        //_sb.Begin(
        //    samplerState: SamplerState.PointWrap,
        //    transformMatrix: m);
        //_testConwin.Draw(_sb, new Vector2(100, 100));
        //_sb.End();

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
            _player.ActionRight();
        }

        if (KeyWasPressed(Keys.A) || KeyWasPressed(Keys.Left))
        {
            _player.ActionLeft();
        }

        if (KeyWasPressed(Keys.W) || KeyWasPressed(Keys.Up))
        {
            _player.ActionUp();
        }

        if (KeyWasPressed(Keys.S) || KeyWasPressed(Keys.Down))
        {
            _player.ActionDown();
        }

        if (KeyWasPressed(Keys.Space) || KeyWasPressed(Keys.RightControl))
        {
            _player.SelectTool(PlayerTool.Mine);
        }
    }

    private bool KeyWasPressed(Keys key)
    {
        return _prevState.IsKeyUp(key) && _currState.IsKeyDown(key);
    }
}

