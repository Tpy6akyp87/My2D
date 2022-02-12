using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Monster
{
    public float distanceToPlayer;
    public float distanceAttack;
    public float speed;
    private SpriteRenderer sprite;
    private Animator animator;
    public CharSCR player;
    private GameObject blood;
    private int lives = 5;
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        blood = Resources.Load<GameObject>("BloodDamage");
    }
    protected override void Update()
    {
        player = FindObjectOfType<CharSCR>();
        //Проверка на преследование
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceToPlayer && (gameObject.transform.position - player.transform.position).magnitude > distanceAttack)
            TothePlayer();
        //Проверка на круговую атаку
        if ((gameObject.transform.position - player.transform.position).magnitude <= distanceAttack)
            Attack();
    }
    //private bool IsPlayerNear
    //{
    //    get { return animator.GetBool("IsPlayerNear"); }
    //    set { animator.SetBool("IsPlayerNear", value); }
    //}

    public void TothePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void Attack()
    {
        player.ReceiveDamage();
        player.State = CharState.RDamage;
        Debug.Log("персонаж получил урон - вызван метод player.ReceiveDamage();");
    }
    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }
    public override void ReceiveDamage()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log("Бык получил урон");
        }  
        
        Vector3 position = transform.position;
        GameObject newBlood = Instantiate(blood, position, blood.transform.rotation) as GameObject;
    }
}
