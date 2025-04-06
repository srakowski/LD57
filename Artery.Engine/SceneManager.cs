namespace Artery.Engine;

class SceneManager : GameComponent
{
    private readonly List<Scene> _scenes = [];
    private readonly ArteryEngine _engine;

    public SceneManager(ArteryEngine engine) : base(engine.Game)
    {
        _engine = engine;
        Game.Components.Add(this);
    }

    internal IEnumerable<Scene> Scenes => _scenes;
}
