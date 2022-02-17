using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject panelDialog;
    public GameObject thoseWhoSay;
    public Text textCS;
    public string textMessage;
    public string[] message;
    public bool dialogStart = false;
    public bool wordIsDone = false;
    private int i = 0;
    public int skolkoFraz;

    public void Start()
    {
        skolkoFraz = message.Length;
        panelDialog.SetActive(false);
        //message[0] = "Мерзкие гоблины стащили пиво, как быть?!";
        //message[1] = "Статуя: Что за суета? Ты пришёл пиво на меня лить - так лей!";
        //message[2] = "Гоблины телегу унесли... Аааа! Говорящий камень!";
        //message[3] = "Статуя: Чего орёшь?  Сам ты камень, я Вольфрик, и криками Вольфрика не напоить, беги за пивом";
        //message[4] = "Статуя: Топор мой возьми, он скучает по гоблинским задницам";
    }
    private void DialogFade()
    {
        panelDialog.SetActive(false);
    }
    private void Print() { StartCoroutine(PrintMessagePerTime(message[i])); }
    IEnumerator PrintMessagePerTime(string text) 
    {
        string startText = message[i];
        for (int k = 0; k <= text.Length; k++)
        {
            textCS.text = text.Substring(0, k);
            if (k == text.Length) wordIsDone = true;
            yield return new WaitForSeconds(0.08f);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Вошел в диалог");
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(true);
            dialogStart = true;
            Print();
        }
    }
    private void Update()
    {
        panelDialog.transform.position = thoseWhoSay.transform.position;
        if (dialogStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && i < 5 && wordIsDone)
            {
                i++;
                Print();
                wordIsDone = false;
            }
            if (i >= skolkoFraz) Invoke("DialogFade", 2);
        }
    }
}
