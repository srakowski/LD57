namespace Artery.Engine.Rendering;

class SpriteRenderer : Renderer
{
    private string _textureKey;
    private Texture2D _texture;
    private Transform _transform;

    public SpriteRenderer(string textureKey) => _textureKey = textureKey;

    public string TextureKey
    {
        get => _textureKey;
        set
        {
            _textureKey = value;
            RefreshTexture();
        }
    }    

    public Rectangle? SourceRectangle { get; set; }

    public Color Color { get; set; }

    public Vector2 Origin { get; set; }

    public SpriteEffects SpriteEffects { get; set; }

    public float LayerDepth { get; set; }

    protected override void OnActivated()
    {
        RefreshTexture();
        _transform = Entity.GetComponent<Transform>();
        base.OnActivated();
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        if (_texture is null || _transform is null)
        {
            return;
        }

        spriteBatch.Draw(
            _texture,
            _transform.Position,
            SourceRectangle,
            Color,
            _transform.Rotation,
            Origin,
            _transform.Scale,
            SpriteEffects,
            LayerDepth);
    }

    private void RefreshTexture()
    {
        if (Scene?.Content is null) return;
        _texture = Scene.Content.Load<Texture2D>(_textureKey);
    }
}
