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
        if (player.Lives < 1)
        {
            Resurrection();
        }
    }
    public void Resurrection()
    {

        {
            character.transform.position = respawnPoint.transform.position;
            player.Lives = 2;
        }
    }
    //public void OnTriggerEnter2D(Collider2D collider)
    //{
    //    Unit unit = collider.GetComponent<Unit>();
    //    if (unit && unit is CharSCR)
    //    {
    //        unit.ReceiveDamage();
    //        character.transform.position = respawnPoint.transform.position;
    //    }
    //}
}
