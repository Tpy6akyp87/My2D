using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHight : Monster
{
    [SerializeField]
    private float rate = 2.0F;
    private float speed = 25.0F;
    private Bullet bullet;
    [SerializeField]
    private Color bulletColor = Color.white;
    private SpriteRenderer sprite;
    private CharSCR pers;
    [SerializeField]
    private Transform target;


    protected override void Awake()
    {

        bullet = Resources.Load<Bullet>("Bullet");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    public void Shoot()
    {
        pers = FindObjectOfType<CharSCR>();
        Vector3 position = transform.position;
        if (gameObject.transform.position.x < pers.transform.position.x) sprite.flipX = true;
        else sprite.flipX = false;
        position.x += (sprite.flipX ? -1.0F : 1.0F) * 0.1F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = target.transform.position-position;
        newBullet.Color = bulletColor;
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
