using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : UnitC
{
    public float speed;
    public float jumpForce;
    //public int lives;
    private void Start()
    {
        StartLife(lives);
    }
    private void Update()
    {
        if (Input.GetButton("Horizontal")) //движение
        {
            Run(speed, GetComponentInChildren<SpriteRenderer>());
        }
        if (Input.GetButtonDown("Jump")) //прыжок
        {
            Jump(jumpForce, GetComponent<Rigidbody2D>());
        }
        if (Input.GetButtonDown("Fire2")) //стрельба
        {
            Shoot(GetComponentInChildren<SpriteRenderer>());
        }
    }
}
