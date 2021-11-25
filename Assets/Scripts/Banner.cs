using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
    public void Update()
    {
        Invoke("Destroy(gameObject)",4);
    }

   
}
