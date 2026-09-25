using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Player playerScript;

    // Carga la escena del juego (asegúrate de poner el nombre exacto de tu escena)
    public void Resume()
    {
        playerScript.Resume();
    }

    // Cierra el juego (funciona en la compilación final)
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartingMenu");
    }
}