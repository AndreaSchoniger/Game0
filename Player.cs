using System.Numerics;
using Raylib_cs;

public class Player
// Player es una clase porque define las propiedades y los comportamientos de los objetos.
{
    public Vector2 pos = new(200, 200);
    float speed = 200;

    public int maxHealth = 100;
    public int currentHealth = 100;

    public void Draw()
    {
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, 20, 50, new Color(230, 41, 55, 255));
        
        DrawHealthBar();
    }

    // Draw es un método que forma parte de la clase Player.
    // Un método es una función que realiza una acción sobre los datos de los objetos.

    public void Move()
    {
        Vector2 dir = new Vector2();

        if (Raylib.IsKeyDown(KeyboardKey.W)) dir.Y = -1;
        else if (Raylib.IsKeyDown(KeyboardKey.S)) dir.Y = 1;
        else dir.Y = 0;

        if (Raylib.IsKeyDown(KeyboardKey.A)) dir.X = -1;
        else if (Raylib.IsKeyDown(KeyboardKey.D)) dir.X = 1;
        else dir.X = 0;

        if (dir.X != 0 || dir.Y != 0)
        {
            dir = Vector2.Normalize(dir);
        }

        pos += dir * speed * Raylib.GetFrameTime();
    }

    void DrawHealthBar()
    {
        int barWidth = 60;
        int barHeight = 8;
        int barX = (int)pos.X - 20;
        int barY = (int)pos.Y - 15;
        
        // Fondo rojo de la barra.
        Raylib.DrawRectangle(barX, barY, barWidth, barHeight, new Color(200, 0, 0, 255));
        
        // Barra verde según la vida actual.
        int greenWidth = (int)((float)currentHealth / maxHealth * barWidth);
        Raylib.DrawRectangle(barX, barY, greenWidth, barHeight, new Color(0, 200, 0, 255));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}