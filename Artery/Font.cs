namespace Artery;

using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

class Font
{
    public static Dictionary<char, Rectangle> Map { get; }
    public static Texture2D Texture { get; internal set; }

    public static int CharWidth = 8;
    
    public static int CharHeight = 8;

    static Font()
    {
        Map = [];
        for (var c = 'A'; c < 'Z' + 1; c++)
        {
            var p = c - 'A';
            var rect = new Rectangle(8 * p, 0, 8, 8);
            Map.Add(c, rect);
            Map.Add(char.ToLower(c), rect);
        }

        int i = 0;
        for (i = 0; i < 10; i++)
        {
            var rect = new Rectangle(8 * i, 8, 8, 8);
            Map.Add(i.ToString().First(), rect);
        }

        Map.Add(':', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add(';', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('<', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('=', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('>', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('?', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('-', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('.', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('[', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add('\\', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add(']', new Rectangle(8 * i, 8, 8, 8)); i++;
        Map.Add(' ', new Rectangle(8 * 25, 8, 8, 8)); i++;
    }

    public static Rectangle GetSourceRectangle(char c)
    { 
        if (Map.TryGetValue(c, out var rect))
        {
            return rect;
        }

        return Map[' '];
    }
}
