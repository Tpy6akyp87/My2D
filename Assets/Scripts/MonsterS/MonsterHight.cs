using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterHight : Monster
{
    private GameObject blood;
    [SerializeField]
    private float rate = 2.0F;
    private float speed = 2.0F;
    private Bullet bullet;
    [SerializeField]
    private Color bulletColor = Color.white;
    private SpriteRenderer sprite;
    private CharSCR pers;
    [SerializeField]
    private Transform target;
    private Vector3 direction;
    private Vector3 directionV;
    private Animator animator;
    [SerializeField]
    private float shootdistance;


    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        bullet = Resources.Load<Bullet>("Bullet");
        sprite = GetComponentInChildren<SpriteRenderer>();
        blood = Resources.Load<GameObject>("BloodDamage");

        pers = FindObjectOfType<CharSCR>();
    }

    protected override void Start()
    {
        Vector3 startposition = gameObject.transform.position;
        InvokeRepeating("Shoot", rate, rate);
        InvokeRepeating("ChangeDirection", rate, rate);
        InvokeRepeating("ChangeDirectionV", rate * 0.7F, rate * 0.2F);
        direction = transform.right;
        directionV = transform.up;
        State = 0;
    }
    protected override void Update()
    {
        Move();
    }

    private void ChangeDirection()
    {
        direction *= -1.0F;
    }
    private void ChangeDirectionV()
    {
        directionV *= -1.0f;
    }

    public void Shoot()
    {
        if (Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) <= shootdistance && Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) <= shootdistance)
        {
            Vector3 position = transform.position;
            if (gameObject.transform.position.x < pers.transform.position.x) sprite.flipX = true;
            else sprite.flipX = false;
            position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.1F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = target.transform.position - position;
            newBullet.Color = bulletColor;
            newBullet.speed = 15.0F;
        }
    }
    private void Move()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * (-0.5F) + transform.right * direction.x * 0.6F, 0.05F);
        //if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<CharSCR>())) direction *= -1.0F;
        
        sprite.flipX = direction.x > 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction + directionV, speed * Time.deltaTime);
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
        Vector3 position = transform.position;
        GameObject newBlood = Instantiate(blood, position, blood.transform.rotation) as GameObject;
        Destroy(newBlood, 1.0F);
        // Invoke("Die",0.4F);
    }
}
