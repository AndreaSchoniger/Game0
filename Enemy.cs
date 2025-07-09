using System.Numerics;
using Raylib_cs;

public class Enemy
{
    public Vector2 pos = new(150, 200);
    float speed = 150; // Un poco más lento que el Player.
    float stopDis = 50; // Distancia mínima para dejar de seguir al Player.

    public int maxHealth = 80;
    public int currentHealth = 80;

    float attackDistance = 70;
    int attackDamage = 20;
    float attackCooldown = 1.0f;
    float currentCooldown = 0;
    bool isAttacking = false;
    float attackDuration = 0.5f;
    float currentAttackTime = 0;

    public void Draw()
    {
        Raylib.DrawRectangle((int)pos.X, (int)pos.Y, 20, 50, new Color(55, 170, 200, 255));
        
        DrawHealthBar();

        if (isAttacking)
        {
            DrawSword();
        }
    }

    // Follow es un método que mueve al Enemy hacia el Player si la distancia entre ellos es mayor que la distancia mínima (stopDis).
    public void Follow(Player player)
    {
        if (isAttacking)
        {
            return;
        }
        
        Vector2 dir = Vector2.Normalize(player.pos - pos);
        Vector2 offset = dir * stopDis;
        Vector2 dest = player.pos - offset;

        const float epsilon = 0.3f;

        if (Vector2.Distance(dest, pos) > epsilon)
        {
            pos += dir * speed * Raylib.GetFrameTime();
        }
        else
        {
            pos = dest;
        }
    }

    public void Update(Player player)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Raylib.GetFrameTime();
        }

        if (isAttacking)
        {
            currentAttackTime -= Raylib.GetFrameTime();
            if (currentAttackTime <= 0)
            {
                isAttacking = false;
            }
        }
        
        float distanceToPlayer = Vector2.Distance(pos, player.pos);
        if (distanceToPlayer <= attackDistance && currentCooldown <= 0 && !isAttacking)
        {
            // Atacar al Player si está dentro del rango de ataque y no se está atacando.
            Attack(player);
        }
        
        if (!isAttacking)
        {
            // Seguir al Player si no se está atacando.
            Follow(player);
        }
    }
    
    void Attack(Player player)
    {
        isAttacking = true;
        currentAttackTime = attackDuration;
        currentCooldown = attackCooldown;
        
        // Hacer daño al jugador.
        player.TakeDamage(attackDamage);
    }
    
    void DrawHealthBar()
    {
        int barWidth = 60;
        int barHeight = 8;
        int barX = (int)pos.X - 20;
        int barY = (int)pos.Y - 15;
        
        // Fondo rojo de la barra.AAAA
        Raylib.DrawRectangle(barX, barY, barWidth, barHeight, new Color(200, 0, 0, 255));
        
        // Barra verde según la vida actual.
        int greenWidth = (int)((float)currentHealth / maxHealth * barWidth);
        Raylib.DrawRectangle(barX, barY, greenWidth, barHeight, new Color(0, 200, 0, 255));
    }
    
    void DrawSword()
    {
        int swordX = (int)pos.X + 25;
        int swordY = (int)pos.Y + 10;
        
        // Dibujar la espada como un rectángulo gris.
        Raylib.DrawRectangle(swordX, swordY, 25, 5, Color.Gray);
    }
}