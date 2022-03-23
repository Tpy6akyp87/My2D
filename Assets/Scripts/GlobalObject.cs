using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    private Resurrect respoint;
    public static GlobalObject Instance;
    public int lives;
    public int patrons;
    public int barrels;
    void Awake()
    {
        respoint = FindObjectOfType<Resurrect>();
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            gameObject.transform.position = respoint.transform.position;
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
