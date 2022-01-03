using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;
    private float startposition;
    private Vector3 direction;
    public void startPositioner()
    {
        startposition = transform.position.x;
    }

    private void Start()
    {
        startPositioner();
        direction = transform.right;
    }

    private void Update()
    {
    Move();
    }
    private void Move ()
    {
    if (Mathf.Abs(transform.position.x - startposition)>2.0F) direction *= -1.0F;
    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed*Time.deltaTime);
    }
}
