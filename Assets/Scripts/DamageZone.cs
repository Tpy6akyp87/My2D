using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private CharSCR player;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = FindObjectOfType<CharSCR>();
        if (player)
        {
            player.ReceiveDamage();
            Debug.Log(player.Lives);
        }
    }

}
