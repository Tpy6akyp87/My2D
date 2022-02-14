using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private GameObject sparkles;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        CharSCR character = collider.GetComponent<CharSCR>();

        if (character)
        {
            character.Barrels++;
            Debug.Log(character.Barrels);
            Destroy(gameObject);
            Vector3 position = transform.position;
            GameObject newSparkle = Instantiate(sparkles, position, sparkles.transform.rotation) as GameObject;
            Destroy(newSparkle, 1.5F);
        }
    }
}
