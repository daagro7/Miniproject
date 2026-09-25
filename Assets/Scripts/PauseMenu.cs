using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Player playerScript;

    /**
     * Resume the game
     */
    public void Resume()
    {
        playerScript.Resume();
    }

    /**
     * Quit the game
     */
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartingMenu");
    }
}