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
        Vector3 startposition = gameObject.transform.position;
        InvokeRepeating("ChangeDirection", speed, speed);
        direction = transform.right;
    }
    private void ChangeDirection()
    {
        direction *= -1.0F;
    }

    private void Update()
    {
    Move();
    }
    private void Move ()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
