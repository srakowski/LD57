namespace Artery;

using System.Collections.Generic;

public enum Tile
{
    LeftInput = 0,
    Text,
    RightOutput,
    LeftCap,
    RightCap,
}

class Tiles
{
    public static Dictionary<Tile, Rectangle> Map { get; }
    public static Texture2D Texture { get; internal set; }

    public static int UnitWidth = 8;

    public static int UnitHeight = 11;

    static Tiles()
    {
        Map = [];

        void Add(Tile tile)
        {
            Map.Add(tile, new Rectangle(8 * (int)tile, 0, 8, 11));
        }

        Add(Tile.LeftInput);
        Add(Tile.Text);
        Add(Tile.RightOutput);
        Add(Tile.LeftCap);
        Add(Tile.RightCap);
    }

    public static Rectangle GetSourceRectangle(Tile tile) => Map[tile];
}
