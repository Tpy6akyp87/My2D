using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpider : Monster
{
    private Animator animator;
    private SpriteRenderer sprite;
    public CharSCR player;
    private GameObject blood;
    private Vector3 direction;
    public float distanceAttack;
    public float speed;
    private int lives = 5;
    public float distanceFollowPlayer;
    public float shootdistance = 10.5F;
    private Bullet bullet;
    public Color bulletColor = Color.white;
    private float dist;
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
    }
    protected override void Start()
    {
        direction = transform.right;
        player = FindObjectOfType<CharSCR>();
    }
    //Idle-0
    //Attack-1
    //Shoot-2
    //Die-3
    protected override void Update()
    {
        dist = (gameObject.transform.position - player.transform.position).magnitude;
        if (dist <= shootdistance && dist >= distanceAttack)
            Shoot();
        if (dist <= distanceFollowPlayer && dist > distanceAttack)// && State != 3)
            TothePlayer();
        if (dist <= distanceAttack)// && State != 3)
            if (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < 2) // ѕроверка на топот
                State = 3;
            else
                transform.position = Vector3.MoveTowards(transform.position, -player.transform.position, speed * Time.deltaTime);
    }
    public void TothePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (gameObject.transform.position.x > player.transform.position.x)
            sprite.flipX = true;
        else
            sprite.flipX = false;
        State = 1;
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
    }
}
