using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpider : Monster
{
    private Animator animator;
    private SpriteRenderer sprite;
    public CharSCR player;
    private GameObject blood;
    //private Vector3 direction;
    private Vector3 downVector;
    public float distanceAttack;
    public float speed;
    private int lives = 5;
    public float distanceFollowPlayer;
    public float shootdistance = 10.5F;
    private Bullet bullet;
    public Color bulletColor = Color.white;
    private float dist;
    private float vertDist;
    public GameObject key;
    private bool runing;
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
        //direction = transform.right;
        player = FindObjectOfType<CharSCR>();
    }
    //Idle-0
    //Attack-1
    //Shoot-2
    //Die-3
    protected override void Update()
    {
        dist = (gameObject.transform.position - player.transform.position).magnitude;
        vertDist = Mathf.Abs(gameObject.transform.position.y - player.transform.position.y);


        if (dist <= shootdistance && dist > distanceAttack && State != 3 && !runing) //стрелба
            State = 2;


        if (dist <= distanceAttack && State != 3) //отбег  && vertDist > 0.2f
            FromthePlayer();
        else
            runing = false;


        if (dist <= distanceAttack && vertDist <= 0.2f && State != 3) //атака
                State = 1;
    }
    public void FromthePlayer()
    {
        if (vertDist > 3 && gameObject.transform.position.y > player.transform.position.y && dist > 4) 
            downVector = Vector3.down;
        else
            downVector = Vector3.zero;

        transform.position = -Vector3.MoveTowards(transform.position, player.transform.position + downVector, speed * 3.0f * Time.deltaTime);

        if (gameObject.transform.position.x > player.transform.position.x) // проверка на поворот к игроку
            sprite.flipX = true;
        else
            sprite.flipX = false;

        State = 0;
        runing = true;
        
    }
    //public void Landing()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, Vector3.down, speed * Time.deltaTime);
    //}
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
