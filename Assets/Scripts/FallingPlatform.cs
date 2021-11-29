using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Color Color
    {
        get { return Color.white; }
        set { sprite.color = value; }
    }
    public virtual void DestroyPlatform()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
        
    }
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public virtual void CreatePlatform()
    {
        //Destroy(gameObject);
        gameObject.SetActive(true);
        sprite.color.a.Equals(0.2);
    }
    public virtual void FullTR()
    {
        sprite.color.a.Equals(0.4);
        sprite.color.a.ToString("0.4");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Character"))
        {
            Invoke("DestroyPlatform", 1);
            Invoke("CreatePlatform", 4);
            Invoke("FullTR", 5);
        }
    }
}
