using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoof : Monster
{
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
    }

   
    public void Shoot()
    {
        pers = FindObjectOfType<CharSCR>();
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
        }
    }
    
}
