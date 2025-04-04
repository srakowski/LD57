namespace LD57.Engine;

abstract class GameSystem(GameEngine engine) : GameComponent(engine.Game)
{
    protected GameEngine Engine { get; } = engine;
}