using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    //private CharSCR player;
    [SerializeField]
    private string nextscene = null;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //player = FindObjectOfType<CharSCR>();
        if (collision.tag == "Player")
        {
            //player.SavePlayer();
            SceneManager.LoadScene(nextscene);
        }
    }
}
