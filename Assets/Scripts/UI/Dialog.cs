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
    private CharSCR pers;
    private void Start()
    {
       // panelDialog.transform.position = thoseWhoSay.transform.position;
        panelDialog.SetActive(false);
        message[0] = "������� ������� ������� ����, ��� ����?!";
        message[1] = "������: ��� �� �����? �� ������ ���� �� ���� ���� - ��� ���!";
        message[2] = "������� ������ ������... ����! ��������� ������!";
        message[3] = "������: ���� ����?  ��� �� ������, � ��������, � ������� ��������� �� �������, ���� �� �����";
        message[4] = "������: ����� ��� ������, �� ������� �� ���������� ��������";
    }
    private void DialogFade()
    {
        panelDialog.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����� � ������");
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(true);
            dialogStart = true; 
            textCS.text = message[0];
        }
        if (collision.tag == "Chest")
        {
            pers.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("����� �� �������");
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
