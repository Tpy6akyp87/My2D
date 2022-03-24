using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public int count;
    private GameObject sparkles;
    private void Awake()
    {
        sparkles = Resources.Load<GameObject>("FlagSparkle");
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        CharSCR character = collider.GetComponent<CharSCR>();

        if (character)
        {
            character.Barrels+= count;
            Debug.Log(character.Barrels);
            Destroy(gameObject);
            Vector3 position = transform.position;
            GameObject newSparkle = Instantiate(sparkles, position, sparkles.transform.rotation) as GameObject;
            Destroy(newSparkle, 1.5F);
            //character.SavePlayer();
        }
    }
}
