﻿namespace Artery;

class Conwin
{
    private readonly ConwinCell[,] _buffer;

    private Point _cursorPos = Point.Zero;

    private readonly int _width;

    private readonly int _height;

    public Vector2 Pos;

    public Conwin(int rows, int columns)
    {
        _buffer = new ConwinCell[columns, rows];
        _width = columns;
        _height = rows;

        for (var x = 0; x < columns; x++)
        {
            for (var y = 0; y < rows; y++)
            {
                _buffer[x, y].Value = null;
            }
        }
    }

    public void Write(string value)
    {
        foreach (var c in value)
        {
            _buffer[_cursorPos.X, _cursorPos.Y].Value = c;
            AdvanceCursorX();
        }
    }

    public void WriteLine(string value)
    {
        foreach (var c in value)
        {
            _buffer[_cursorPos.X, _cursorPos.Y].Value = c;
            AdvanceCursorX();
        }
        AdvanceCursorY();
    }

    private void AdvanceCursorX()
    {
        _cursorPos.X++;
        if (_cursorPos.X >= _width)
        {
            AdvanceCursorY();
        }
    }

    private void AdvanceCursorY()
    {
        _cursorPos.X = 0;
        _cursorPos.Y++;
        if (_cursorPos.Y >= _height)
        {
            for (var y = 0; y < _height - 1; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    _buffer[x, y] = _buffer[x, y + 1];
                }
            }

            for (var x = 0; x < _width; x++)
            {
                _buffer[x, _height - 1] = new();
            }

            _cursorPos.Y--;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (Font.Texture is null)
        {
            return;
        }

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var v = _buffer[x, y];

                if (!v.Value.HasValue)
                {
                    continue;
                }

                sb.Draw(
                    Font.Texture,
                    Pos + new Vector2(x * Font.CharWidth, y * (Font.CharHeight + 4)),
                    Font.GetSourceRectangle(v.Value.Value),
                    v.Color ?? Color.White
                );
            }
        }
    }
}

struct ConwinCell
{
    public char? Value;
    public Color? Color;
}