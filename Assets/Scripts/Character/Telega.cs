using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telega : MonoBehaviour
{
    public GameObject Thievs;
    public float speed = 2.0F;
    private GameObject sparkles;
    public bool runRight = false;
    private void Awake()
    {
        sparkles = Resources.Load<GameObject>("Smoke");
    }
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run(); //Debug.Log(State);
        }
        if (runRight)
        {
            MobileRunRight();
        }

    }
    public void MobileRunRight()
    {
        runRight = true;
        Vector3 direction = transform.right;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    public void MobileStop()
    {
        runRight = false;
    }
    public void ActivateThiefs()
    {
        Thievs.SetActive(true);
    }
    public void DestroyTelega()
    {
        Destroy(gameObject);
        Thievs.SetActive(false);
        GameObject newSparkle = Instantiate(sparkles, gameObject.transform.position, sparkles.transform.rotation) as GameObject;
        Destroy(newSparkle, 1.5F);
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
