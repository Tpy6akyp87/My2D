using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telega : MonoBehaviour
{
    public GameObject Thievs;
    public float speed = 2.0F;
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run(); //Debug.Log(State);
        }
        
    }
    public void ActivateThiefs()
    {
        Thievs.SetActive(true);
    }
    public void DestroyTelega()
    {
        Destroy(gameObject);
        Thievs.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dialog2")
        {
            Debug.Log("Телега прила к смерти");
            Invoke("DestroyTelega",2.3F);
            speed = 0.0F;
            Invoke("ActivateThiefs",1);
        }
    }
    
    public void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        if (direction.x > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
    }
}
