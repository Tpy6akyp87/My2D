using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSCR : Unit
{
    [SerializeField]
    private int lives = 100;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= 5) lives = value;
            healthBar.Refresh();
        }
    }
    private HealthBar healthBar;

    [SerializeField]
    private float speed = 5.0F;
    [SerializeField]
    public float jumpForce = 25.0F; //всегда менять и у лестницы в скрипте

    private bool isGrounded = false;

    private Bullet bullet;

    new public Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    

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
        CheckGround();
    }
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    
    private void Update()
    {
        State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButton("Vertical")) Climb();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (rigidbody.velocity.y < 0) State = CharState.Fall;
        if (rigidbody.velocity.y > 0) State = CharState.Jump;
        if (Input.GetButtonDown("Fire1") && !PauseMenu.GameIsPaused) Shoot();
        if (lives ==0) SceneManager.LoadScene("Menu");// добавить экран геймовер
        
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
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
        position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.1F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    } 

    private void Jump()
    {
        
        rigidbody.velocity = new Vector3(0, jumpForce, 0);
    }

    public override void ReceiveDamage()
    {
        Lives--;
        rigidbody.AddForce(transform.up * 4.0F, ForceMode2D.Impulse);
        Debug.Log(lives);
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

}

public enum CharState{
    Idle,
    Run,
    Jump,
    Fall
}
