using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneCharSCR : Unit
{
    [SerializeField]
    private float speedwalk = 2.0F;
    new public Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private CharStateC State
    {
        get { return (CharStateC)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Update()
    {
        State = CharStateC.Idle;

        if (Input.GetButton("Horizontal")) Run();
    }
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        State = CharStateC.Run;
        if (direction.x > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speedwalk * Time.deltaTime);
            //sprite.flipX = direction.x < 0;
        }
        //else sprite.flipX = direction.x < 0;
    }
  
}

public enum CharStateC
{
    Idle,
    Run
}

