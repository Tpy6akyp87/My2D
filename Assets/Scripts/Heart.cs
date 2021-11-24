using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        CharSCR character = collider.GetComponent<CharSCR>();

        if (character)
        {
            character.Lives++;
            Debug.Log(character.Lives);
            Destroy(gameObject);
        }
    }
}
