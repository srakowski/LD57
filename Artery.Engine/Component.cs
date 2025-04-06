namespace Artery.Engine;

abstract class Component
{
    internal void Activate(ArteryEngine engine, Scene scene, Entity entity)
    {
        Engine = engine;
        Scene = scene;
        Entity = entity;
        OnActivated();
    }

    internal ArteryEngine Engine { get; private set; }

    internal Scene Scene { get; private set; }

    internal Entity Entity { get; private set; }

    protected virtual void OnActivated() { }
}
