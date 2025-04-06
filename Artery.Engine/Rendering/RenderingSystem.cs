namespace Artery.Engine.Rendering;

using System;

class RenderingSystem : DrawableGameComponent
{
    private readonly Dictionary<Scene, List<Renderer>> _renderersByScene = [];
    private readonly ArteryEngine _engine;
    private SpriteBatch _sb;

    public RenderingSystem(ArteryEngine engine) : base(engine.Game)
    {
        _engine = engine;
        Game.Components.Add(this);
    }

    public override void Initialize()
    {
        base.Initialize();
        _sb = new SpriteBatch(Game.GraphicsDevice);
    }

    public void RegisterRenderer(Renderer renderer)
    {
        (_renderersByScene[renderer.Scene] ??= []).Add(renderer);
    }

    public void Draw()
    {
        foreach (var scene in _engine.SceneManager.Scenes)
        {
            DrawScene(scene);
        }
    }

    private void DrawScene(Scene scene)
    {
        var layers = _renderersByScene[scene]
            .GroupBy(r => r.Layer?.Depth ?? 0)
            .OrderBy(d => d.Key);

        foreach (var layer in layers)
        {
            _sb.Begin();
            foreach (var renderer in layer)
            {
                renderer.Draw(_sb);
            }
            _sb.End();
        }
    }
}
