using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<CharSCR>().transform;
    }
    private void Update()
    {
        Vector3 position = target.position;
        position.z = -50.0F; 
        //position.y = target.position.y + 2.0F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);

    }
}
