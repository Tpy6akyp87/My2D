using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautyFire : MonoBehaviour
{
    public GameObject objectRnd;
    private float timeBtwSpark;
    private float startTimeBtwSpark;
    private CharSCR pers;
    //public Transform summPosition;
    private void Awake()
    {
        startTimeBtwSpark = Random.Range(3.5f, 5.5f);
        objectRnd = Resources.Load<GameObject>(objectRnd.name.ToString());
        pers = FindObjectOfType<CharSCR>();
    }
    private void Sparkle() 
    {
        int var = Random.Range(1,3);
        if (var == 2)
        {
            GameObject gameObject = Instantiate(objectRnd, transform.position, objectRnd.transform.rotation) as GameObject;
            Destroy(gameObject,1.0f);
        }
    }
    private void FixedUpdate()
    {if ((pers.transform.position - gameObject.transform.position).magnitude < 10.0f)
        {
            if (timeBtwSpark <= 0)
            {
                Sparkle();
                timeBtwSpark = startTimeBtwSpark;
            }
            else
            {
                timeBtwSpark -= Time.deltaTime;
            }
        }
    }
}
