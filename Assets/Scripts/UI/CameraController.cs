using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;
    //[SerializeField]
    //private Transform target;
    private CharSCR pers;

    private void Awake()
    {
        pers = FindObjectOfType<CharSCR>();
    }
    private void Update()
    {
        Vector3 position = pers.transform.position;
        position.z = -50.0F;
        if (pers.isFlip) position.x = position.x + 3.0F;
        else position.x = position.x - 3.0F;
        //position.y = position.y + 2.0F;
        //position.y = target.position.y + 2.0F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);

    }
}
