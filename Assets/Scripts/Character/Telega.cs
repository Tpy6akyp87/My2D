using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telega : MonoBehaviour
{
    
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dialog2")
        {
            Destroy(gameObject);
        }
        
    }
}
