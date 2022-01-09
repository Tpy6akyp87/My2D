using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShoot : Monster
{
    [SerializeField]
    private float shootdistance;
    private Bullet bullet;
    [SerializeField]
    private Color bulletColor = Color.white;
    private SpriteRenderer sprite;
    private CharSCR pers;
    private Animator animator;
    private float timeBtwShoot;
    public float startTimeBtwShoot;


    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        bullet = Resources.Load<Bullet>("Bullet");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    protected override void Update()
    {
        pers = FindObjectOfType<CharSCR>();
        if (gameObject.transform.position.x < pers.transform.position.x) sprite.flipX = false;
        else sprite.flipX = true;
        IsPlayerNear = false;

        if (timeBtwShoot <= 0)
        {
            if (Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) <= shootdistance)
            {
                IsPlayerNear = true;
                //Shoot();
                timeBtwShoot = startTimeBtwShoot;
            }
            else IsPlayerNear = false;
        }
        else
        {
            timeBtwShoot -= Time.deltaTime;
        }
    }

    protected override void Start()
    {
        
    }

    private bool IsPlayerNear
    {
        get { return animator.GetBool("IsPlayerNear"); }
        set { animator.SetBool("IsPlayerNear", value); }
    }
    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    public void Shoot()
    {
        pers = FindObjectOfType<CharSCR>();
        Vector3 position = transform.position;
        if (gameObject.transform.position.x < pers.transform.position.x) sprite.flipX = false; 
        else sprite.flipX = true;
        position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.1F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        newBullet.Color = bulletColor;
    }

    public override void ReceiveDamage()
    {
        Debug.Log("смерть пришла");
        State = 0;
       // Invoke("Die",0.4F);
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is CharSCR)
        {
            unit.ReceiveDamage();
            Debug.Log("ShootMonsterDamage");
        }
    }
}
