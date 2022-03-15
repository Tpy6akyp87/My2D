using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSSD : Monster
{
    private GameObject blood;
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
        blood = Resources.Load<GameObject>("BloodDamage");
        pers = FindObjectOfType<CharSCR>();
    }
    //0-idle
    //1-run
    //2-attack
    //3-die

    protected override void Update()
    {
        if ((gameObject.transform.position - pers.transform.position).magnitude <= attackdistance && State !=3) OnAttack();
        if ((gameObject.transform.position - pers.transform.position).magnitude > attackdistance && State != 3) Move();

        if (gameObject.transform.position.x < pers.transform.position.x && State != 3) sprite.flipX = false;
        else sprite.flipX = true;

    }
    protected override void Start()
    {
        State = 0;
    }
    private void Move()
    {
        
        if((gameObject.transform.position-pers.transform.position).magnitude < 5 && Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) < 1.5F && State != 3)
        {
            speed = 4.0f;
            direction = pers.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, direction, speed * Time.deltaTime);
            State = 1;
        }
        if ((gameObject.transform.position - pers.transform.position).magnitude >= 5 && State != 3) 
        {
            speed = 0.0f;
            State = 0;
        }
    }
    public void OnAttack()
    {
        if (timeBtwAtack <= 0)
        {
            State = 2;
            pers.ReceiveDamage();
            speed = 0.0F;
            timeBtwAtack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAtack -= Time.deltaTime;
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
        State = 3;
        speed = 0.0F;
        Vector3 position = transform.position;
        GameObject newBlood = Instantiate(blood, position, blood.transform.rotation) as GameObject;
        Destroy(newBlood, 1.0F);
    }
}
