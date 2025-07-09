using System.Numerics;
using Raylib_cs;

public class Enemy
{
    public enum State
    {
        Idle, Chase, Attack, Defeat
    }

    public enum AttackState
    {
        Cooldown, // etc...
    }

    public Player m_Target; // una posible referencia a cualquier m_Target.

    public State m_State = State.Idle;
    public AttackState m_AttackState = AttackState.Cooldown;
    public State m_PrevState = State.Idle;

    public Vector2 m_Pos = new(150, 200);
    float speed = 150; // Un poco más lento que el m_Target.
    float m_StopDis = 50; // Distancia mínima para dejar de seguir al m_Target.

    public int maxHealth = 80;
    public int currentHealth = 80;

    float attackDistance = 70;
    int attackDamage = 20;
    float attackCooldown = 1.0f;
    float m_CurrAttackCooldown = 0;
    float attackDuration = 0.5f;
    float m_CurrAttackDuration = 0;

    // ----------------- UPDATE --------------------

    public void Update()
    {
        if (m_Target == null)
        {
            m_State = State.Idle;
        }

        if (m_State != m_PrevState)
        {
            // hacer cualquier cosa que implique un cambio de estado
            m_PrevState = m_State;
        }

        // State machine
        switch (m_State)
        {
            case State.Idle:
                UpdateIdleState();
                break;
            case State.Chase:
                UpdateChaseState();
                break;
            case State.Attack:
                UpdateAttackState();
                break;
        }
    }

    // ----------------- UPDATE STATES --------------------

    public void UpdateIdleState()
    {
        if (m_Target != null)
        {
            m_State = State.Chase;
        }
    }

    public void UpdateChaseState()
    {
        Vector2 dir = Vector2.Normalize(m_Target.pos - m_Pos);
        Vector2 offset = dir * m_StopDis;
        Vector2 dest = m_Target.pos - offset;

        const float EPSILON = 0.3f;

        if (Vector2.Distance(dest, m_Pos) > EPSILON)
        {
            m_Pos += dir * speed * Raylib.GetFrameTime();
        }
        else
        {
            m_Pos = dest;
            m_State = State.Attack;
        }
    }

    public void UpdateAttackState()
    {
        void Attack()
        {
            m_CurrAttackDuration = attackDuration;
            m_CurrAttackCooldown = attackCooldown;

            // Hacer daño al jugador.
            m_Target.TakeDamage(attackDamage);
        }

        switch (m_AttackState)
        {
            case AttackState.Cooldown:
                break;
        }

        m_CurrAttackDuration -= Raylib.GetFrameTime();

        if (m_CurrAttackDuration <= 0)
        {
            //dedidir si pasamos a idle o seguimos atacando
        }

        if (m_CurrAttackCooldown > 0)
        {
            m_CurrAttackCooldown -= Raylib.GetFrameTime();
        }

        float distanceTom_Target = Vector2.Distance(m_Pos, m_Target.pos);
        if (distanceTom_Target <= attackDistance && m_CurrAttackCooldown <= 0 && !isAttacking)
        {
            // Atacar al m_Target si está dentro del rango de ataque y no se está atacando.
            Attack();
        }
    }

    // ----------------- DRAW STUFF --------------------

    public void Draw()
    {
        Raylib.DrawRectangle((int)m_Pos.X, (int)m_Pos.Y, 20, 50, new Color(55, 170, 200, 255));

        DrawHealthBar();

        if (m_State == State.Attack)
        {
            DrawSword();
        }
    }
    
    void DrawHealthBar()
    {
        int barWidth = 60;
        int barHeight = 8;
        int barX = (int)m_Pos.X - 20;
        int barY = (int)m_Pos.Y - 15;
        
        // Fondo rojo de la barra.AAAA
        Raylib.DrawRectangle(barX, barY, barWidth, barHeight, new Color(200, 0, 0, 255));
        
        // Barra verde según la vida actual.
        int greenWidth = (int)((float)currentHealth / maxHealth * barWidth);
        Raylib.DrawRectangle(barX, barY, greenWidth, barHeight, new Color(0, 200, 0, 255));
    }
    
    void DrawSword()
    {
        int swordX = (int)m_Pos.X + 25;
        int swordY = (int)m_Pos.Y + 10;
        
        // Dibujar la espada como un rectángulo gris.
        Raylib.DrawRectangle(swordX, swordY, 25, 5, Color.Gray);
    }
}