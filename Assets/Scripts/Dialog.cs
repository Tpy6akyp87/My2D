using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject panelDialog;
    public GameObject thoseWhoSay;
    private void Start()
    {
       // panelDialog.transform.position = thoseWhoSay.transform.position;
        panelDialog.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Вошел в диалог");
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Вышел из диалога");
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(false);
        }
    }
}
