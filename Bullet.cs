using System.Numerics;
using Raylib_cs;

public class Bullet
{
    public Vector2 pos;
    public Vector2 dir;
    public float speed = 400;
    public float hitDistance = 10f;
    public int damage = 10;

    public Bullet(Vector2 startPos, Vector2 direction)
    {
        pos = startPos;
        dir = Vector2.Normalize(direction);
    }

    public void Update()
    {
        pos += dir * speed * Raylib.GetFrameTime();
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)pos.X, (int)pos.Y, 5, Color.Yellow);
    }
}