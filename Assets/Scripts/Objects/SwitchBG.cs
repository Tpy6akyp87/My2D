using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBG : MonoBehaviour
{
    public GameObject skywallpaper;
    public GameObject cavewallpaper;
    private void Start()
    {
        cavewallpaper.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            skywallpaper.SetActive(false);
            cavewallpaper.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skywallpaper.SetActive(true);
            cavewallpaper.SetActive(false);
        }
    }
}
