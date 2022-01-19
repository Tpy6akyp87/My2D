using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrect : MonoBehaviour
{
    [SerializeField]
    private Transform character = null;
    [SerializeField]
    private Transform respawnPoint = null;
    private CharSCR player;

    public void Update()
    {
        player = FindObjectOfType<CharSCR>();
        if (player.dieTrigger == true)
        {
            Invoke("Resurrection", 5.0F);
        }
    }
    public void Resurrection()
    {
        Debug.Log("Респавн");
        player.transform.position = respawnPoint.transform.position;
    }
}
