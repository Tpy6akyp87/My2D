using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CutScene");
    }
    public void ExitGame()
    {
        Debug.Log("GAME OVER");
        Application.Quit();
    }
    public void PlayScene1()
    {
        SceneManager.LoadScene("FirstScene");
    }
    public void PlayScene2()
    {
        SceneManager.LoadScene("SecondScene");
    }
    public void PlayScene3()
    {
        SceneManager.LoadScene("ThirdScene");
    }
    public void PlayScene4()
    {
        SceneManager.LoadScene("FourhScene");
    }
}
