using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Player playerScript;

    /**
     * Restart the game
     */
    public void Restart()
    {
        management("GameScene");
    }

    /**
     * Quit the game
     */
    public void Quit()
    {
        management("StartingMenu");
    }

    public void management(String scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
