using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject panelDialog;
    public GameObject thoseWhoSay;
    public Text textCS;
    public string[] message;
    public bool dialogStart = false;
    public int i = 0;
    private void Start()
    {
       // panelDialog.transform.position = thoseWhoSay.transform.position;
        panelDialog.SetActive(false);
        message[0] = "Мерзкие гоблины стащили пиво, как быть?!";
        message[1] = "Статуя: Что за суета? Ты пришёл пиво на меня лить - так лей!";
        message[2] = "Гоблины телегу унесли... Аааа! Говорящий камень!";
        message[3] = "Статуя: Чего орёшь?  Сам ты камень, я Вольфрик, и криками Вольфрика не напоить, беги за пивом";
        message[4] = "Статуя: Топор мой возьми, он скучает по гоблинским задницам";
    }
    private void DialogFade()
    {
        panelDialog.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Вошел в диалог");
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(true);
            dialogStart = true; 
            textCS.text = message[0];
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Вышел из диалога");
        if (collision.tag == "Player")
        {
            Invoke("DialogFade",2);
        }
    }
    private void Update()
    {
        if (dialogStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && i < 5)
            {
                i++;
                textCS.text = message[i];
            }
           if (i>=6) Invoke("DialogFade", 2);
        }
    }
}
