using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;        //Can check if the game is paused from anywhere!
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Use the escape key to pause or resume the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Function for when the game is resumed
    //Make the cursor invisible, remove the pause menu UI, resume time
    public void Resume()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //Function for when the game is paused
    //Show the cursor, enable the pause menu UI, freeze time
    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //Load the menu (for load menu button)
    public void loadMenu()
    {
        print("IM LOADING THE MENU");
        //SceneManager.LoadScene("MainMenu")
    }

    //Quit the game (for quit button)
    public void QuitGame()
    {
        print("IM QUITTING THE GAME");
        //Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
