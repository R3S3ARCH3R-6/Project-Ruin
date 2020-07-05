using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Scene_Changer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //The following code is used to allow the mouse to appear only on certain screens
        Cursor.visible = true;
            //the statement above allows the mouse to be seen
        Cursor.lockState = CursorLockMode.None;
            //the above statment stops the mouse from being locked into place or disabled
    }

    /// <summary>
    /// Quits the game entirely. This script stops the program/game.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// This function brings the user to the scene "Dead Scene" when they die in the game.
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene("Dead Screen 2");
    }

    /// <summary>
    /// This function brings the user to the scene "Win Scene" when they beat the game.
    /// </summary>
    public void Victory()
    {
        SceneManager.LoadScene("Victory Scene 1");
    }


    public void PlayerSelect()
    {
        SceneManager.LoadScene("Floor One");
    }

    public void DemoScene()
    {
        SceneManager.LoadScene("AI Build and Controls");
    }

    /// <summary>
    /// 
    /// </summary>
    public void EntryLevel1()
    {
        //energy.playerScore = 0;
        SceneManager.LoadScene("Entry Level 1");
    }

    /// <summary>
    /// Brings player to the main menu/opening scene in the game
    /// </summary>
    public void StartScreen()
    {
        //energy.playerScore = 0;
        SceneManager.LoadScene("Start Screen 2");
        Time.timeScale = 1f;
    }
}
