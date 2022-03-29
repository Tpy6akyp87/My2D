using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;
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
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
