using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeStart = GlobalObject.time;
    public Text timerText;
    void Start()
    {
        timeStart = GlobalObject.time;
        timerText.text = timeStart.ToString();
    }
    void Update()
    {
        timeStart += Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString();
    }
}
