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
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position = FindObjectOfType<Resurrect>().transform.position;
    }
}
