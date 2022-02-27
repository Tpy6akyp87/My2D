using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautyFire : MonoBehaviour
{
    public GameObject objectRnd;
    private float timeBtwSpark;
    public float startTimeBtwSpark;
    //public Transform summPosition;
    private void Awake()
    {
        objectRnd = Resources.Load<GameObject>(objectRnd.name.ToString());
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
