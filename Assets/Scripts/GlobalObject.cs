using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    //private CameraController cameraController;
    public static GlobalObject Instance;
    public static int lives = 5;
    public static int patrons = 6;
    public static int barrels = 0;
    public static float time = 0;
    private void Start()
    {
        gameObject.transform.position = FindObjectOfType<Resurrect>().transform.position;
    }
    void Awake()
    {
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        //if (objs.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}

        //DontDestroyOnLoad(this.gameObject);
        gameObject.transform.position = FindObjectOfType<Resurrect>().transform.position;

        //if (Instance == null)
        //{
        //    Debug.Log("�� ��������");
        //    DontDestroyOnLoad(gameObject);
        //    Instance = this;
        //}
        //else if (Instance != this) // ����� ������� else ��� if
        //{
        //    Debug.Log("��������");
        //    Destroy(gameObject);
        //}

    }
}
