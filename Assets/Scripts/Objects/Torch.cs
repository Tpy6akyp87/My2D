using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private float rate;
    private float speed;
    private Vector3 direction;
    private Vector3 directionV;
    private SpriteRenderer sprite;
    private void Awake()
    {
        rate = Random.Range(1.5f,2.5f);
        speed = Random.Range(0.5f,0.9f);
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        Vector3 startposition = gameObject.transform.position;
        InvokeRepeating("ChangeDirection", rate, rate);
        InvokeRepeating("ChangeDirectionV", rate * 0.7F, rate * 1.2F);
        direction = transform.right;
        directionV = transform.up;
    }
    private void Update()
    {
        Move();
    }

    private void ChangeDirection()
    {
        direction *= -1.0F;
    }
    private void ChangeDirectionV()
    {
        directionV *= -1.0f;
    }
    private void Move()
    {
        sprite.flipX = direction.x > 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction + directionV, speed * Time.deltaTime);
    }
}
