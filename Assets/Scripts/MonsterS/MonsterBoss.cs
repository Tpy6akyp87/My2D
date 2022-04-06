using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoss : Monster
{
    public float distanceToPlayer;
    public float distanceAttack;
    public float distForBattleBegin;
    public float speed;
    public float powerOfStomp;
    private Vector3 direction;
    private SpriteRenderer sprite;
    private Animator animator;
    private CharSCR player;
    private Bullet bullet;
    [SerializeField]
    private Color bulletColor = Color.white;
    private GameObject blood;
    public GameObject ground;
    public GameObject key;
    public GameObject frostWall;
    public int lives = 20;
    private bool isBattleBegin = false;
    public float distBtw;
    public float timeBtwShoot;
    public float startTimeBtwShoot;
    public GameObject addableMonster;
    public Transform summPosition;



    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        blood = Resources.Load<GameObject>("BloodDamage");
        bullet = Resources.Load<Bullet>("Bullet");
    }
    //Idle-0
    //Run-1
    //Attack-2
    //ReceiveDamage-3
    //Die-4
    protected override void Start()
    {
        direction = transform.right;
        player = FindObjectOfType<CharSCR>();
    }
    protected override void Update()
    {
        distBtw = (gameObject.transform.position - player.transform.position).magnitude;

        //триггер начала боя
        if (distBtw < distForBattleBegin && State != 4)
        {
            frostWall.SetActive(true);
            isBattleBegin = true; 
        }

        //Проверка на преследование
        if (distBtw <= distanceToPlayer && distBtw > distanceAttack && State != 4)
            TothePlayer();
        
        //проверка на обстрел
        if (timeBtwShoot <= 0 && State != 4 && isBattleBegin || timeBtwShoot <= 0 && distBtw >= distanceToPlayer && State != 4 && isBattleBegin)
        {
            UpperShoot();
            timeBtwShoot = startTimeBtwShoot;
        }
        else
        {
            timeBtwShoot -= Time.deltaTime;
        }

        //Проверка ближнюю атаку
        if (distBtw <= distanceAttack && State != 4)
        {
            speed = 0.0f;
            State = 2;
        }

        //проверка смерти игрока
        if (player.Lives < 1 && State != 4) 
        {
            isBattleBegin = false;
            State = 0;
        }

    }


    public void TothePlayer()
    {
        speed = 4.8f;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (gameObject.transform.position.x > player.transform.position.x)
            sprite.flipX = true;
        else
            sprite.flipX = false;
        State = 1;
    }
    public void Attack()
    {
        if (distBtw <= distanceAttack)
        {
            player.ReceiveDamage();
            player.State = CharState.RDamage;
            Debug.Log("персонаж получил урон ВБЛИЗИ");
        }
    }
   
    public void UpperShoot()//опустил землю
    {
        for (int i = 0; i <= 7; i++)
        {
            Bullet newBullet = Instantiate(bullet, new Vector3(543.0f + Random.Range(0, 19), 227.0f, 0.0f), bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.speed = 6.0f;
            newBullet.Direction = Vector3.down;
            newBullet.Color = bulletColor;
        }

        ground.transform.position = new Vector3(0, -1 * powerOfStomp);
        Debug.Log("ПУЛИ ПОШЛИ!");
    }
    public void Stomp1()//вернул землю
    {
        ground.transform.position = new Vector3(0, 0);
    }

    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }
    public override void ReceiveDamage()
    {
        lives--;
        if (lives > 0)
        {
            isBattleBegin = true;
        }
        else
        {
            Debug.Log("босс УМЕР!");
            State = 4;
            speed = 0.0F;
            Debug.Log("State is   " + State);
            key.SetActive(true);
            frostWall.SetActive(false);
        }
        if (lives == 20 || lives == 15 || lives == 10 || lives == 5)
        {
            GameObject gameObject = Instantiate(addableMonster, transform.position + new Vector3(2.0f,0.0f,0.0f), addableMonster.transform.rotation) as GameObject;
        }
        Debug.Log("у босса жизней   " + lives);
        Vector3 position = transform.position;
        GameObject newBlood = Instantiate(blood, position, blood.transform.rotation) as GameObject;
        Destroy(newBlood, 1.0F);
    }
}
