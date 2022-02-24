using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitC : MonoBehaviour
{
    public int lives;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= 5) lives = value;
            //healthBar.Refresh();
        }
    }
    public virtual void StartLife(int startlives)
    {
        
    }
    public virtual void Run(float speed, SpriteRenderer sprite)
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;
    }
    public virtual void Jump(float jumpForce, Rigidbody2D rigidbody)
    {
        rigidbody.velocity = new Vector3(0, jumpForce, 0);
    }
    public virtual void Shoot(SpriteRenderer sprite)
    {
        Vector3 position = transform.position;
        position.y += -0.3F + 0.132F - 0.07F+1.0f;
        position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.9F;
        Bullet newBullet = Instantiate(Resources.Load<Bullet>("Bullet1"), position, Resources.Load<Bullet>("Bullet1").transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        //Patrons--;
        //patronBar.Refresh();
    }
    public virtual void ReceiveDamage(int damage)
    {
        Lives-= damage;
        if (Lives == 0)
        {
            Die();
        }
        if (Lives > 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
    public virtual void Die() 
    {
        Destroy(gameObject);
    }

}
