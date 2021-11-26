using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public virtual void DestroyPlatform()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
    public virtual void CreatePlatform()
    {
        //Destroy(gameObject);
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Character"))
        {
            Invoke("DestroyPlatform", 1);
            Invoke("CreatePlatform", 4);
        }
    }
}
