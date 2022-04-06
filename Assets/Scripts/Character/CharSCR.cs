using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSCR : Unit
{
    [SerializeField]
    private int patrons = 6;
    private int lives = 500;
    private int barrels = 0;
    public float time;
    private HealthBar healthBar;
    private PatronBar patronBar;
    private BarrelPanel barrelPanel;
    [SerializeField]
    public float speed = 5.0F;
    [SerializeField]
    public float jumpForce = 13.0F; //всегда менять и у лестницы в скрипте
    private bool isGrounded = false;
    private Bullet bullet;
    new public Rigidbody2D rigidbody;
    public Animator animator;
    private SpriteRenderer sprite;
    int playerObject, platformObject,groundLayer;
    public bool isFlip = false;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;

    private float timeBtwAtack;
    public float startTimeBtwAttack;
    private float timeBtwShoot;
    public float startTimeBtwShoot;
    public float timerDamage;

    private float takeDamage;
    public bool dieTrigger;
    public Timer timer;

    public int Barrels
    {
        get { return barrels; }
        set
        {
            if (value <= 3) barrels = value;
            barrelPanel.Refresh();
        }
    }
    public int Patrons
    {
        get { return patrons; }
        set
        {
            if (value <= 6) patrons = value;
            patronBar.Refresh();
        }
    }
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
        Patrons = 6;
        playerObject = LayerMask.NameToLayer("Player");
        platformObject = LayerMask.NameToLayer("Platforms");
        groundLayer = LayerMask.NameToLayer("Ground");
        Lives = GlobalObject.lives;
        Patrons = GlobalObject.patrons;
        Barrels = GlobalObject.barrels;
        time = GlobalObject.time;
    }
    public void Awake()
    {
        patronBar = FindObjectOfType<PatronBar>();
        barrelPanel = FindObjectOfType<BarrelPanel>();
        healthBar = FindObjectOfType<HealthBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet1");
        timer = FindObjectOfType<Timer>();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Update()
    {
        
        if (State != CharState.Die)
            State = CharState.Idle;
        dieTrigger = false;
        timerDamage -= Time.deltaTime;

        if (Input.GetButton("Horizontal") && State != CharState.Die) //движение
        { 
            Run();
        }
        if (Input.GetButton("Vertical") && State != CharState.Die) //карабкаться
        { 
            Climb();
        }
        if (takeDamage == 1) // если получил урон
        {
            State = CharState.RDamage;
            takeDamage = 0;
        }
        if (isGrounded && Input.GetButtonDown("Jump") && State != CharState.Die) //прыжок
        {
            Jump();
        }
        if (takeDamage == 2) //если умер
        {
            State = CharState.Die;
            Debug.Log("Помер");
            Invoke("Ressurrect", 5.0F);
            dieTrigger = true;
            takeDamage = 0;
        }
        
        if (rigidbody.velocity.y < 0 && State != CharState.Die)
        {
            Physics2D.IgnoreLayerCollision(playerObject, platformObject, false);
            //State = CharState.Fall;
        }
        if (rigidbody.velocity.y > 0 && State != CharState.Die)
        {
            //State = CharState.Jump;
            Physics2D.IgnoreLayerCollision(playerObject, platformObject, true);
        }
        //Debug.Log(timeBtwShoot);
        if (timeBtwShoot <= 0 && Patrons > 0 && State != CharState.Die)
        {
            if (Input.GetButtonDown("Fire2") && !PauseMenu.GameIsPaused)
            {
                Debug.Log("Выстрел");
                State = CharState.Shoot;
                //Shoot();
                timeBtwShoot = startTimeBtwShoot;
            }
        }
        else
        {
            timeBtwShoot -= Time.deltaTime;
        }
        if (timeBtwAtack <= 0 && State != CharState.Die)// удар
        {
            if (Input.GetButtonDown("Fire1"))
            {
                State = CharState.Meelee;
                timeBtwAtack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAtack -= Time.deltaTime;
        }
        time = timer.timeStart;


    }
    public void Ressurrect()
    {
        State = CharState.Idle;
        Lives = 2;
        Patrons = 3;
        speed = 5.0F;
        speed = 5.0F;
        Debug.Log("я воскрес");
        takeDamage = 0;
    }

    public void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        if (direction.x < 0) attackPos.transform.position = gameObject.transform.position + new Vector3(-0.33F, -0.34F, 0);
        if (direction.x > 0) attackPos.transform.position = gameObject.transform.position + new Vector3(0.33F, -0.34F, 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
        isFlip = !sprite.flipX;
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
        if (Patrons > 0)
        {
            Vector3 position = transform.position;
            position.y += -0.3F + 0.132F - 0.07F;
            position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.9F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
            Patrons--;
            patronBar.Refresh();
        }
        Debug.Log("Осталось" + patrons + "патронов");
        SavePlayer();
    }
    private void Jump()
    {
        rigidbody.velocity = new Vector3(0, jumpForce, 0);
    }
    public override void ReceiveDamage()
    {
        if (timerDamage < 0) //1 сек неуязвимости после получения урона
        {
            //Lives--;
            timerDamage = 1.0F;
        }

        if (Lives == 0)
        {
            takeDamage = 2;
            speed = 0.0F;
        }
        if (Lives>0)
        {
            takeDamage = 1;
            Debug.Log(lives);
        }
        SavePlayer();
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
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void SavePlayer()
    {
        GlobalObject.lives = Lives;
        GlobalObject.patrons = Patrons;
        GlobalObject.barrels = Barrels;
        GlobalObject.time = time;
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
