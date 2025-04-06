namespace Artery;

public enum PlayerTool
{ 
    None = 0,
    Mine,
}


class Player
{
    public Point _worldPos;

    public PlayerTool _currentTool = PlayerTool.None;

    public Mine _mine;


    public Player(int worldX, int worldY)
    {
        _worldPos = new Point(worldX, worldY);
        _mine = new Mine(_worldPos.X, _worldPos.Y);
    }

    public static Texture2D Texture { get; set; }

    internal void ActionLeft()
    {
        _worldPos.X--;
        _mine.WorldPos.X--;
    }

    internal void ActionRight()
    {
        _worldPos.X++;
        _mine.WorldPos.X++;
    }

    internal void ActionUp()
    {
        _worldPos.Y--;
        _mine.WorldPos.Y--;
    }

    internal void ActionDown()
    {
        _worldPos.Y++;
        _mine.WorldPos.Y++;
    }

    public void SelectTool(PlayerTool tool)
    {
        _currentTool = tool;
    }

    public void Draw(SpriteBatch sb)
    {
        if (Texture is null) return;

        if (_currentTool == PlayerTool.None)
        {
            var drawPos = _worldPos.ToVector2() * Region.CellPosMultiplier;
            sb.Draw(
                Texture,
                drawPos,
                Color.White);
        }
        else if (_currentTool == PlayerTool.Mine)
        {
            _mine.Draw(sb);
        }
    }
}
