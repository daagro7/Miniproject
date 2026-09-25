using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour
{
    // Carga la escena del juego (asegúrate de poner el nombre exacto de tu escena)
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Cierra el juego (funciona en la compilación final)
    public void CloseAsteroid()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
