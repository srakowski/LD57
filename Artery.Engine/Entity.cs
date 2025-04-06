namespace Artery.Engine;

class Entity
{
    private readonly List<Component> _components = [];

    private ArteryEngine _engine;

    public Entity()
    {
        AddComponent(new Transform());
    }

    internal Scene Scene { get; private set; }

    internal void Activate(ArteryEngine engine, Scene scene)
    {
        _engine = engine;
        Scene = scene;

        foreach (var component in _components)
        {
            component.Activate(_engine, Scene, this);
        }
    }

    internal void AddComponent(Component component)
    {
        _components.Add(component);

        if (_engine is not null)
        {
            component.Activate(_engine, Scene, this);
        }
    }

    internal T GetComponent<T>() where T : Component
    {
        return _components.OfType<T>().FirstOrDefault();
    }
}