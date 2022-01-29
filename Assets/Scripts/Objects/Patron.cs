using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        CharSCR character = collider.GetComponent<CharSCR>();

        if (character)
        {
            character.Patrons++;
            Debug.Log(character.Patrons);
            Destroy(gameObject);
        }
    }
}
