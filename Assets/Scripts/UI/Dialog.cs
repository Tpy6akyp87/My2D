using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject panelDialog;
    public GameObject thoseWhoSay;
    public GameObject clickImg;
    public Text textCS;
    public string textMessage;
    public string[] message;
    public bool dialogStart = false;
    public bool wordIsDone = false;
    private int i = 0;
    public int skolkoFraz;

    public void Start()
    {
        panelDialog.SetActive(false);
    }
    private void DialogFade()
    {
        panelDialog.SetActive(false);
    }
    private void Print() { StartCoroutine(PrintMessagePerTime(message[i])); }
    IEnumerator PrintMessagePerTime(string text) 
    {
        for (int k = 0; k <= text.Length; k++)
        {
            textCS.text = text.Substring(0, k);
            if (k == text.Length)
            {
                clickImg.SetActive(true);
                wordIsDone = true; 
            }
            yield return new WaitForSeconds(0.08f);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Вошел в диалог");
        if (collision.tag == "Player")
        {
            skolkoFraz = message.Length;
            panelDialog.SetActive(true);
            dialogStart = true;
            Print();
        }
    }
    private void Update()
    {
        panelDialog.transform.position = thoseWhoSay.transform.position+ new Vector3(0.0f,0.5f,0.0f);
        if (dialogStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && i < skolkoFraz-1 && wordIsDone)
            {
                i++;
                Print();
                wordIsDone = false;
                clickImg.SetActive(false);
            }
            if (i == skolkoFraz-1 && wordIsDone) 
            { 
                Invoke("DialogFade", 2);
                dialogStart = false;
                clickImg.SetActive(false);
            }
            if (i == skolkoFraz-1)
            {
                clickImg.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("DialogFade", 2);
            dialogStart = false;
        }
    }
}
