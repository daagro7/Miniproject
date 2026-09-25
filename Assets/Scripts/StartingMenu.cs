using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour
{
    /**
     * Load a new game
     */
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    /**
     * Close the application
     */
    public void CloseAsteroid()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
