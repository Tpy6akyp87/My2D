using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSCR : Unit
{
    [SerializeField]
    private int lives = 5;
    private HealthBar healthBar;
    [SerializeField]
    public float speed = 5.0F;
    [SerializeField]
    public float jumpForce = 13.0F; //всегда менять и у лестницы в скрипте
    private bool isGrounded = false;
    private Bullet bullet;
    new public Rigidbody2D rigidbody;
    public Animator animator;
    private SpriteRenderer sprite;
    int playerObject, platformObject;
    public bool isFlip = false;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;

    private float timeBtwAtack;
    public float startTimeBtwAttack;
    private float timeBtwShoot;
    public float startTimeBtwShoot;

    private float takeDamage;

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= 5) lives = value;
            healthBar.Refresh();
        }
    }
    private void Start()
    {
        playerObject = LayerMask.NameToLayer("Player");
        platformObject = LayerMask.NameToLayer("Platforms");
    }
    public void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void FixedUpdate()
    {
        takeDamage = 0;
        CheckGround();
    }
    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Update()
    {
        State = CharState.Idle;
        if (Input.GetButton("Horizontal")) //движение
        { 
            Run();
        }
        if (Input.GetButton("Vertical")) //карабкаться
        { 
            Climb();
        }
        if (takeDamage == 1) // если получил урон
        {
            State = CharState.RDamage; 
        }
        if (takeDamage == 2) //если умер
        {
            State = CharState.Die;
        }


        if (isGrounded && Input.GetButtonDown("Jump")) //прыжок
        { 
            Jump();
        }
        if (rigidbody.velocity.y < 0)
        {
            Physics2D.IgnoreLayerCollision(playerObject, platformObject, false);
            //State = CharState.Fall;
        }
        if (rigidbody.velocity.y > 0)
        {
            //State = CharState.Jump;
            Physics2D.IgnoreLayerCollision(playerObject, platformObject, true);
        }
        //Debug.Log(timeBtwShoot);
        if (timeBtwShoot <= 0)
        {
            if (Input.GetButtonDown("Fire2") && !PauseMenu.GameIsPaused)
            {
                State = CharState.Shoot;
                //Shoot();
                timeBtwShoot = startTimeBtwShoot;
            }
        }
        else
        {
            timeBtwShoot -= Time.deltaTime;
        }
        if (timeBtwAtack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("жамк");
                State = CharState.Meelee;
                //OnAttack();
                timeBtwAtack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAtack -= Time.deltaTime;
        }


    }

    public void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        if (direction.x < 0) attackPos.transform.position = gameObject.transform.position + new Vector3(-0.88F, -0.36F, 0);
        if (direction.x > 0) attackPos.transform.position = gameObject.transform.position + new Vector3(0.88F, -0.36F, 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
        State = CharState.Run;
    }
    public void Climb()
    {
        Vector3 direction = transform.up * Input.GetAxis("Vertical");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
        State = CharState.Run;
    }
    public void Shoot()
    {
        Vector3 position = transform.position; 
        position.y += -0.5F;
        position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.9F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }
    //public void Attack()
    //{
    //    Debug.Log("жамк");
    //    State = CharState.Meelee;
    //}
    private void Jump()
    {
        rigidbody.velocity = new Vector3(0, jumpForce, 0);
    }
    public override void ReceiveDamage()
    {
        Lives--;
        if (Lives <= 0)
        {
            takeDamage = 2;
            State = CharState.Die;
            speed = 0.0F;
        }
        else
        {
            takeDamage = 1;
            Debug.Log(lives);
        }
    }
    private void CheckGround() 
    {
        isGrounded = rigidbody.velocity.y == 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Moving Platforms")) this.transform.parent = collision.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Moving Platforms")) this.transform.parent = null;
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Monster>().ReceiveDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }




}

public enum CharState{
    Idle,
    Run,
    Jump,
    Fall,
    Meelee,
    RDamage,
    Shoot,
    Die,
    Res
}
