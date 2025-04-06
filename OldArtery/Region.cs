namespace Artery;

class Region
{
    public const int CellWidthInPixels = 12;

    public const int CellHeightInPixels = 12;

    public static readonly Vector2 CellPosMultiplier = new(CellWidthInPixels, CellHeightInPixels);

    private readonly Vector2 _offset;

    private Dictionary<Point, NaturalResource> NaturalResources = [];

    public Region(Rectangle bounds)
    {
        Bounds = bounds;
        _offset = new Vector2(bounds.X * CellHeightInPixels, bounds.Y * CellHeightInPixels);

        NaturalResources[new Point(23, 23)] = new Iron();
        NaturalResources[new Point(23, 43)] = new Copper();
    }

    public Rectangle Bounds { get; }

    public static Texture2D CellTexture { get; set; }

    public void Draw(SpriteBatch sb)
    {
        if (CellTexture is null)
        { 
            return; 
        }

        for (var y = 0; y < Bounds.Width; y++)
        {
            for (var x = 0; x < Bounds.Height; x++)
            {
                sb.Draw(
                    CellTexture,
                    new Vector2(x, y) * CellPosMultiplier,
                    new Color(20, 20, 20));
            }
        }

        foreach (var kv in NaturalResources)
        {
            kv.Value.Draw(sb, kv.Key.ToVector2() * CellPosMultiplier);
        }
    }
}

public abstract class NaturalResource
{
    private Rectangle SourceRect { get; }

    protected NaturalResource(Rectangle sourceRectangle)
    {
        SourceRect = sourceRectangle;
    }

    public void Draw(SpriteBatch sb, Vector2 at)
    {
        if (Resources.Texture is null)
        {
            return;
        }

        sb.Draw(Resources.Texture, at, SourceRect, Color.White);
    }
}

public class Iron : NaturalResource
{
    public Iron() : base((new Rectangle(0, 0, 12, 12)))
    {
    }
}

public class Copper : NaturalResource
{
    public Copper() : base((new Rectangle(12, 0, 12, 12)))
    {
    }
}
