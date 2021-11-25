using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        CharSCR unit = collider.GetComponent<CharSCR>();
        if (unit)
        { 
            unit.rigidbody.gravityScale = 0;
           // unit.jumpForce = 0;
            unit.rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        CharSCR unit = collider.GetComponent<CharSCR>();

        if (unit)
        {
            unit.rigidbody.gravityScale = 3;
            unit.jumpForce = 25;
        }
    }
}
