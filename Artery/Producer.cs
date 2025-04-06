namespace Artery;

class Producer
{
    public static Producer Iron(Vector2 position)
    {
        return new Producer
        {
            Name = "Iron",
            Color = Color.DarkRed,
            TextColor = Color.White,
            Position = position
        };
    }

    public static Producer Copper(Vector2 position)
    {
        return new Producer
        {
            Name = "Copper",
            Color = Color.Orange,
            TextColor = Color.Blue,
            Position = position
        };
    }

    public required string Name;

    public required Color Color;

    public required Color TextColor;

    public required Vector2 Position;

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Tiles.Texture, Position, Tiles.GetSourceRectangle(Tile.LeftCap), Color);
        var i = 0;
        for (i = 0; i < Name.Length; i++)
        {
            sb.Draw(Tiles.Texture, Position + new Vector2((i + 1) * 8, 0), Tiles.GetSourceRectangle(Tile.Text), Color);
        }
        sb.Draw(Tiles.Texture, Position + new Vector2((i + 1) * 8, 0), Tiles.GetSourceRectangle(Tile.RightOutput), Color);

        for (i = 0; i < Name.Length; i++)
        {
            sb.Draw(Font.Texture, Position + new Vector2((i + 1) * 8, 1), Font.GetSourceRectangle(Name[i]), TextColor);
        }
    }
}
