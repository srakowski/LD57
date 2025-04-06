namespace Artery.Engine;

using Microsoft.Xna.Framework.Content;

class Scene
{
    private readonly List<Entity> _entities = [];

    private ArteryEngine _engine;

    internal ContentManager Content { get; private set; }

    internal void Activate(ArteryEngine engine)
    {
        _engine = engine;

        Content = new ContentManager(
            _engine.Game.Services,
            _engine.Game.Content.RootDirectory
        );

        foreach (var entity in _entities)
        {
            entity.Activate(_engine, this);
        }
    }

    internal Scene AddEntity(Entity entity)
    {
        _entities.Add(entity);

        if (_engine is not null)
        {
            entity.Activate(_engine, this);
        }

        return this;
    }
}
