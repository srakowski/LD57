namespace Artery;

public class ArteryGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _sb;
    private Conwin _gameConsole;
    private Producer _iron;
    private Producer _copper;
    private Consumer _ironC;
    private Consumer _copperC;

    public ArteryGame()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1440,
        };

        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";

        _gameConsole = new Conwin(rows: 8, columns: 126)
        {
            Pos = new Vector2(8, 768 - (96 + 4))
        };

        _iron = Producer.Iron(new Vector2(100, 100));
        _copper = Producer.Copper(new Vector2(100, 200));

        _ironC = Consumer.Iron(new Vector2(600, 200));
        _copperC = Consumer.Copper(new Vector2(400, 400));
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    private Texture2D _segment;

    protected override void LoadContent()
    {
        _sb = new SpriteBatch(GraphicsDevice);
        Font.Texture = Content.Load<Texture2D>("font");
        Tiles.Texture = Content.Load<Texture2D>("tiles");
        //_segment = Content.Load<Texture2D>("segment");
        _segment = new Texture2D(GraphicsDevice, 1, 1);
        _segment.SetData([Color.White]);
    }

    int i = 0;

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var m = Matrix.Identity * Matrix.CreateScale(3f);
        _sb.Begin(samplerState: SamplerState.PointWrap, transformMatrix: m);
        _gameConsole.Draw(_sb);
        _iron.Draw(_sb);
        _copper.Draw(_sb);
        _ironC.Draw(_sb);
        _copperC.Draw(_sb);
        // DrawCurvedLine(_sb, _segment, new Vector2(200, 200), new Vector2(300, 300));
        _sb.End();

        base.Draw(gameTime);
    }
}
