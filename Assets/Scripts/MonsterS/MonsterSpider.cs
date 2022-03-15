using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpider : Monster
{
    private Animator animator;
    private SpriteRenderer sprite;
    public CharSCR player;
    private GameObject blood;
    private Vector3 directionV;
    private Vector3 direction;
    public float distanceAttack;
    public float speed;
    private int lives = 5;
    public float distanceFollowPlayer;
    public float shootdistance;
    private Bullet bullet;
    public Color bulletColor = Color.white;
    private float dist;
    private float vertDist;
    public GameObject key;
    private bool runing;
    private bool playerSprava;
    private bool playerUp;
    public bool battleBegin = false;
    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        blood = Resources.Load<GameObject>("BloodDamage");
        bullet = Resources.Load<Bullet>("Bullet");
    }
    protected override void Start()
    {
        player = FindObjectOfType<CharSCR>();
        State = 0;
    }
    //Idle-0
    //Attack-1
    //Shoot-2
    //Die-3
    protected override void Update()
    {
        dist = (gameObject.transform.position - player.transform.position).magnitude;
        vertDist = Mathf.Abs(gameObject.transform.position.y - player.transform.position.y);
        playerSprava = gameObject.transform.position.x < player.transform.position.x;
        playerUp = gameObject.transform.position.y < player.transform.position.y;

        if (dist <= 10.0F && !battleBegin) battleBegin = true;

        if (dist <= shootdistance && dist > distanceAttack && State != 3 && !runing && battleBegin) //стрелба
            State = 2;
        else 
            if (State != 3 && battleBegin)Move();
    }
    public void Move()
    {
        if (dist <= distanceAttack && State != 3)//убегание
        {
            speed = 12.0f;
            if (playerSprava) direction = Vector3.left;
            else direction = Vector3.right;
            Debug.Log("www");
            runing = true;
        }

        if (dist > shootdistance && State != 3)//преследование
        {
            speed = 3.0f;
            if (playerSprava) direction = Vector3.right;
            else direction = Vector3.left;
            Debug.Log("qqq");
            runing = true;
        }

        if(dist > distanceAttack && dist <= shootdistance && State !=3)
        {
            direction = Vector3.zero;
            speed = 0.0F;
            Debug.Log("eee");
            runing = false;
        }
        if (playerUp && vertDist <= 3.0f) directionV = Vector3.up;
        else directionV = Vector3.zero;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction + directionV, speed * Time.deltaTime);

        if (gameObject.transform.position.x > player.transform.position.x) // проверка на поворот к игроку
            sprite.flipX = true;
        else
            sprite.flipX = false;

        State = 0;
    }
    public void Attack()
    {
        if (dist <= distanceAttack)
        {
            player.ReceiveDamage();
            player.State = CharState.RDamage;
            Debug.Log("персонаж получил урон от топора");
        }
    }
    public void Shoot()
    {
            Vector3 position = transform.position;
            if (gameObject.transform.position.x < player.transform.position.x) sprite.flipX = false;
            else sprite.flipX = true;
            position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.1F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = player.transform.position - position;
            newBullet.Color = bulletColor;
            newBullet.speed = 15.0F;
        speed = 0.0f;
    }
    public override void ReceiveDamage()
    {
        lives--;
        if (lives > 0)
        {
            Debug.Log("моль получил урон");
        }
        else
        {
            Debug.Log("моль УМЕР!");
            State = 3;
            speed = 0.0F;
            Debug.Log("State is   " + State);
            key.SetActive(true);
        }
        Debug.Log("у моля жизней   " + lives);
        Vector3 position = transform.position;
        GameObject newBlood = Instantiate(blood, position, blood.transform.rotation) as GameObject;
        Destroy(newBlood, 1.0F);
    }
}
