using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    new public Rigidbody2D rigidbody;
    [SerializeField]
    private Transform platform = null;
    [SerializeField]
    private Transform startposition = null;
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }
    
    public virtual void DestroyPlatform()
    {
        gameObject.SetActive(false);
    }
    public virtual void ActiveRB()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
       
    }
    
    public virtual void CreatePlatform()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        platform.transform.position = startposition.transform.position;
        platform.transform.rotation = startposition.transform.rotation;
        gameObject.SetActive(true);
    }
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Character"))
        {

            Invoke("ActiveRB", 0.7F);
            Invoke("DestroyPlatform", 1.5F);
            Invoke("CreatePlatform", 4);
          
        }
    }
}
