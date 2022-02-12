using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectFlag : MonoBehaviour
{
    private GameObject sparkles;
    [SerializeField]
    private Transform respawnPoint = null;
    private void Awake()
    {
        sparkles = Resources.Load<GameObject>("FlagSparkle");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            respawnPoint.transform.position = gameObject.transform.position;
            Vector3 position = transform.position;
            GameObject newSparkle = Instantiate(sparkles, position, sparkles.transform.rotation) as GameObject;
            Destroy(newSparkle, 1.5F);
        }

    }
}
