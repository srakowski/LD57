namespace LD57.Engine;

using LD57.Engine.SceneManagement;

class GameEngine : GameComponent
{
    public GameEngine(Game game) : base(game)
    {
        game.Components.Add(this);
        game.Services.AddService(this);

        var sceneManager = new SceneManagementSystem(this);
        game.Components.Add(sceneManager);
        SceneManager = sceneManager;
    }

    public ISceneManager SceneManager { get; private init; }
}
