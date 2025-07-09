using Raylib_cs;
public class GameManager
// GameManager es una clase porque define las propiedades y los comportamientos de los objetos.
{
    Player playerLeft;
    // playerLeft es una variable del tipo Player. Una varaible es un elemento cuyo contenido puede ser modificado.
    // playerLeft es también un class attribute ya que es una variable dentro de una clase que representa una propiedad de sus objetos.
    // En la línea anterior se está declarando el class attribute playerLeft.

    // Un scope define desde dónde se puede acceder a una variable, método o clase.
    // Por ejemplo, playerLeft es una variable privada que solo puede llamarse dentro de la clase GameManager.
    
    Enemy enemy;

    public GameManager()
    {
        Raylib.InitWindow(1280, 720, "Pong");
        playerLeft = new Player();
        enemy = new Enemy();
        enemy.m_Target = playerLeft; // creando una referencia dentro de enemy al player.
        // En la línea anterior se está definiendo e instanciando el class attribute playerLeft, convirtiéndolo en un objeto.
        // Esto nos permite acceder y modificar los atributos y métodos de este objeto a partir de los de la clase Player.
    }

    // public void Update(float deltaTime);
    // Lo anterior sería una declaración de método porque se está llamando al método sin incluir su lógica.
    // En cambio, el método de abajo es una definición de método porque incluye la lógica.
    public void Update(float deltaTime)
    {
        if (playerLeft.IsAlive())
        {
            playerLeft.Move();
            enemy.Update();
        }
        else
        {
            Pong.quit = true;
        }
    }

    public void Draw()
    {
        playerLeft.Draw();
        enemy.Draw();
    }

    // GameManager, Update y Draw son tres métodos que forman parte de la clase GameManager.
    // Un método es una función que realiza una acción sobre los datos de los objetos.
}