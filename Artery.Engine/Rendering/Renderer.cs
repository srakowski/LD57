namespace Artery.Engine.Rendering;

abstract class Renderer : Component
{
    public Layer Layer { get; }

    protected override void OnActivated()
    {
        Engine.Rendering.RegisterRenderer(this);
    }

    internal abstract void Draw(SpriteBatch spriteBatch);
}
