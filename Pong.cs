using Raylib_cs;

class Pong
// Pong es una clase porque define las propiedades y los comportamientos de los objetos.

{
    public static void Main()
    // Main es un static method.
    // Un static method o variable pertenece a una clase y no a una instancia, se puede usar sin crear un objeto.
    {
        // gameManager es una instancia/objeto de la clase Game Manager.
        // bgColor es una instancia/objeto de la clase Color.
        GameManager gameManager = new GameManager();
        Color bgColor = new Color(200, 200, 200, 255);
        // Esto nos permite acceder y modificar los atributos y métodos de estos objetos.

        // while es un loop porque se llama cada frame mientras se cumpla su condición.
        while (Raylib.WindowShouldClose() == false)
        {
            Raylib.ClearBackground(bgColor);

            gameManager.Update(Raylib.GetFrameTime());

            Raylib.BeginDrawing();

            gameManager.Draw();

            Raylib.EndDrawing();
        }

        // if (condición)
        // {
        // Esto es un if statement. Se llama una vez cuando se cumple su condición.
        // }
    }

    // Main es un método que forma parte de la clase Pong.
    // Un método es una función que realiza una acción sobre los datos de los objetos.
}