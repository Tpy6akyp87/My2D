using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUNMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Debug.Log("Жамкнулэск");
                Resume();
            }
            else
            {
                Debug.Log("Жамкнулэск");
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseUNMenuUI.SetActive(false);
        Time.timeScale = 1.0F;
        GameIsPaused = false;
    }
    private void Pause()
    {
        pauseUNMenuUI.SetActive(true);
        Time.timeScale = 0.0F;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1.0F;
        SceneManager.LoadScene("Menu");
        Debug.Log("Жамкнулэск менюскрипт");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
