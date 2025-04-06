namespace Artery.Engine;

using Microsoft.Xna.Framework;

class Transform : Component
{
    public Vector2 Position { get; internal set; }

    public float Scale { get; internal set; }

    public float Rotation { get; internal set; }
}