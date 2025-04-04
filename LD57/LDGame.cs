namespace LD57;

using LD57.Engine;

public class LDGame : Game
{
    private GraphicsDeviceManager _graphics;
    private GameEngine _engine;

    public LDGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _engine = new GameEngine(this);
    }
}
