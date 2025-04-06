namespace Artery;

class Mine
{
    private readonly Conwin _conwin;

    public const int WidthInCells = 3;

    public const int HeightInCells = 3;

    public Point WorldPos;

    public Mine(int worldX, int worldY)
    {
        WorldPos = new Point(worldX, worldY);
        _conwin = new Conwin(4, 4);
        _conwin.Write("MINE");
    }

    public void Draw(SpriteBatch sb)
    {
        _conwin.Draw(sb, WorldPos.ToVector2() * Region.CellPosMultiplier);
    }
}
