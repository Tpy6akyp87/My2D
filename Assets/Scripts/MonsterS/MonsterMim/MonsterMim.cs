using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMim : Monster
{
    public float distanceToPlayer;
    public float distanceAttack;
    public float speed;
    private SpriteRenderer sprite;
    private Animator animator;
    private CharSCR player;
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    protected override void Update()
    {
        TothePlayer();
    }
    private bool IsPlayerNear
    {
        get { return animator.GetBool("IsPlayerNear"); }
        set { animator.SetBool("IsPlayerNear", value); }
    }

    private void TothePlayer()
    {
        player = FindObjectOfType<CharSCR>();
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceToPlayer && (gameObject.transform.position - player.transform.position).magnitude > distanceAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            IsPlayerNear = true;
        }
        else IsPlayerNear = false;
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceAttack)
        {
            IsPlayerNear = true;
        }
    }
    private void Attack()
    {
        player = FindObjectOfType<CharSCR>();
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceAttack)
        {
            player.ReceiveDamage();
        }
    }
}
