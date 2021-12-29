using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telega : MonoBehaviour
{
    public float speed = 2.0F;
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run(); //Debug.Log(State);
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dialog2")
        {
            Debug.Log("Телега прила к смерти");
            Destroy(gameObject);
        }
    }
    
    public void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
