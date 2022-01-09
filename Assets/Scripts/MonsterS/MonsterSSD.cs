using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSSD : Monster
{
    [SerializeField]
    private float speed = 0.5F;
    [SerializeField]
    private float attackdistance;
    private SpriteRenderer sprite;
    private Vector3 direction;
    private CharSCR pers;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    private float timeBtwAtack;
    public float startTimeBtwAttack;
    private Animator animator;


    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private bool IsPlayerNear
    {
        get { return animator.GetBool("IsPlayerNear"); }
        set { animator.SetBool("IsPlayerNear", value); }
    }
    //private bool playernear
    //{
    //    get { return animator.GetBool("playernear"); }
    //    set { animator.SetBool("playernear", value); }
    //}

    protected override void Update()
    {
        AttackCheck();
    }
    protected override void Start()
    {
        direction = transform.right;
    }

    private void AttackCheck() // анимация и таймер между атаками
    {
        pers = FindObjectOfType<CharSCR>();
        if (timeBtwAtack <= 0)
        {
            if (Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) <= attackdistance)
            {
                IsPlayerNear = true;
                speed = 0.0F;
                timeBtwAtack = startTimeBtwAttack;
            }
            if (Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) > attackdistance)
            {
                IsPlayerNear = false;
                Move();
            }
        }
        else
        {
            timeBtwAtack -= Time.deltaTime;
        }
    }
    
    
    private void Move()
    {
        //ускорение к игроку

        pers = FindObjectOfType<CharSCR>();
        if (gameObject.transform.position.x > pers.transform.position.x && Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) < 5 && Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) < 1)
        {
            speed = 4.0F;
            direction = -transform.right;
        }
        if (gameObject.transform.position.x < pers.transform.position.x && Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) < 5 && Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) < 1)
        {
            speed = 4.0F;
            direction = transform.right;
        }
        if (Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) > 5 || Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) > 1) speed = 0.5F;

        //разворот у преграды

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * (-0.5F) + transform.right * direction.x * 0.6F, 0.05F);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<CharSCR>())) direction *= -1.0F;
        sprite.flipX = direction.x < 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<CharSCR>().ReceiveDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        //Gizmos.DrawSphere(attackPos.position, attackRange);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("SSDMonsterDamage");
        }
    }
    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }
    public override void ReceiveDamage()
    {
        Debug.Log("смерть пришла");
        State = 1;
        speed = 0.0F;
        // Invoke("Die",0.4F);
    }
}
