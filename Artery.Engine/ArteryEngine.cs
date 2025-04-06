namespace Artery.Engine;

using Artery.Engine.Rendering;

class ArteryEngine : GameComponent
{
    public ArteryEngine(Game game) : base(game)
    {
        SceneManager = new SceneManager(this);
        Rendering = new RenderingSystem(this);
    }

    public SceneManager SceneManager { get; }

    public RenderingSystem Rendering { get;  }
}
