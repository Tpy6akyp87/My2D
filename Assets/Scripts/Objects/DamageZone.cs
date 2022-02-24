using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1 ;  
    private void OnTriggerEnter2D(Collider2D collider)
    {
       
        CharSCR unit = collider.GetComponent<CharSCR>();
        if (unit)
        {
            for (int i = damage; i > 0; i--)
            {
                unit.ReceiveDamage(); 


            }
           
        }
        Char chara = collider.GetComponent<Char>();
        if (chara)
        {
            for (int i = damage; i > 0; i--)
            {
                chara.ReceiveDamage(1);
            }
        }
    }

}
