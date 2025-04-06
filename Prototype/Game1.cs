using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prototype;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _dot;

    private SpriteFont _dbFont;

    private KeyboardState _currState;
    private KeyboardState _prevState;

    private readonly Player _p1 = new() { Pos = new(0, 0) };
    private readonly Player _p2 = new() { Pos = new(0, 0) };
    private readonly Player _p3 = new() { Pos = new(0, 0) };

    private readonly Player[] _players;

    private readonly float[] _playerMult;

    //private Vector2 _playerPos1 = new(1920 * 0.5f, 1440f);
    //private Vector2 _playerPos2 = new(1920 * 0.5f, 1440f * 0.70f);
    //private Vector2 _playerPos3 = new(1920 * 0.5f, 1440f * 0.40f);

    private Vector2 _playerCam1 = new(1920 * 0.5f, 1440f * 0.8f);
    private Vector2 _playerCam2 = new(1920 * 0.5f, 1440f * 0.6f);
    private Vector2 _playerCam3 = new(1920 * 0.5f, 1440f * 0.4f);

    private int _currentPlayer = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1440,
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _players = [_p1, _p2, _p3];
        _playerMult = [1f, 0.7f, 0.4f];
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _dot = new Texture2D(GraphicsDevice, 1, 1);
        _dot.SetData([Color.White]);

        // TODO: use this.Content to load your game content here

        _dbFont = Content.Load<SpriteFont>("debug");
    }

    protected override void Update(GameTime gameTime)
    {
        _prevState = _currState;
        _currState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        if (_prevState.IsKeyDown(Keys.E) && _currState.IsKeyUp(Keys.E))
        {
            _currentPlayer++;
            _currentPlayer %= 3;
        }

        if (_prevState.IsKeyDown(Keys.Q) && _currState.IsKeyUp(Keys.Q))
        {
            _currentPlayer--;
            _currentPlayer = _currentPlayer < 0 ? 2 : _currentPlayer;
        }

        //var i = _currentPlayer;
        for (var i = _currentPlayer; i < 3; i++)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _players[i].Pos.X -= (0.3f) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _players[i].Pos.X += (0.3f) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _players[i].Pos.Y -= (0.3f) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _players[i].Pos.Y += (0.3f) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(106, 120, 191));

        //// P3

        var t1 = Matrix.Identity
            * Matrix.CreateScale(0.4f)
            * Matrix.CreateTranslation(new Vector3(_playerCam3, 0f));
        //* Matrix.CreateTranslation(new Vector3(_playerCam3, 0f) + (new Vector3(_p1.Pos, 0f) * 0.4f) + (new Vector3(_p2.Pos, 0f) * 0.3f));

        _spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: t1);

        _spriteBatch.Draw(
            _dot,
            new Rectangle((int)_p3.Pos.X, (int)_p3.Pos.Y, 64, 64),
            null,
            new Color(76, 88, 161),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            1f);

        _spriteBatch.Draw(_dot, new Rectangle(-300, 300, 40, 40), null, new Color(76, 88, 161), 0f, Vector2.Zero, SpriteEffects.None, 1f);
        _spriteBatch.Draw(_dot, new Rectangle(300, 300, 40, 40), null, new Color(76, 88, 161), 0f, Vector2.Zero, SpriteEffects.None, 1f);

        _spriteBatch.End();

        /// P2

        var t2 = Matrix.Identity
            * Matrix.CreateScale(0.7f)
            * Matrix.CreateTranslation(new Vector3(_playerCam2, 0f));
        //* Matrix.CreateTranslation(new Vector3(_playerCam2, 0f) + (new Vector3(_p1.Pos, 0f) * 0.7f));

        _spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: t2);

        _spriteBatch.Draw(
            _dot,
            new Rectangle((int)_p2.Pos.X, (int)_p2.Pos.Y, 64, 64),
            null,
            new Color(63, 41, 85),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            1f);

        _spriteBatch.Draw(_dot, new Rectangle(-300, 300, 40, 40), null, new Color(63, 41, 85), 0f, Vector2.Zero, SpriteEffects.None, 1f);
        _spriteBatch.Draw(_dot, new Rectangle(300, 300, 40, 40), null, new Color(63, 41, 85), 0f, Vector2.Zero, SpriteEffects.None, 1f);

        _spriteBatch.End();

        var t3 = Matrix.Identity *
            Matrix.CreateTranslation(new Vector3(_playerCam1, 0f));

        _spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: t3);

        _spriteBatch.Draw(
            _dot,
            new Rectangle((int)_p1.Pos.X, (int)_p1.Pos.Y, 64, 64),
            null,
            new Color(150, 37, 29),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            1f);

        _spriteBatch.Draw(_dot, new Rectangle(-100, 100, 40, 40), null, new Color(150, 37, 29), 0f, Vector2.Zero, SpriteEffects.None, 1f);
        _spriteBatch.Draw(_dot, new Rectangle(100, 100, 40, 40), null, new Color(150, 37, 29), 0f, Vector2.Zero, SpriteEffects.None, 1f);


        _spriteBatch.End();

        //var t3 = Matrix.Identity;// *
        //    //Matrix.CreateTranslation(new Vector3(_playerPos2.X, 0, 0f));

        //_spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: t3);
        //var color = new Color(150, 37, 29);
        //_spriteBatch.Draw(
        //    _dot,
        //    new Rectangle(0, 1200, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 1200),
        //    color);

        //// player forward
        //_spriteBatch.Draw(
        //    _dot,
        //    new Rectangle((int)_playerPos1.X, (int)_playerPos1.Y, 64, 64),
        //    color);

        //_spriteBatch.End();

        _spriteBatch.Begin();
        _spriteBatch.DrawString(
            _dbFont,
            _currentPlayer.ToString(),
            new Vector2(10, 10),
            Color.Black);
        _spriteBatch.End();
    }
}

class Player
{
    public Vector2 Pos;
}