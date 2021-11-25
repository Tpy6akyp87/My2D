using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterMoove : Monster
{
    [SerializeField]
    private float speed = 2.0F;
    private Bullet bullet;
    private SpriteRenderer sprite;
    private Vector3 direction;

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    protected override void Update()
    {
        Move();
    }
    protected override void Start()
    {
        direction = transform.right;
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is CharSCR)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.6F)
            {
                ReceiveDamage();
            }
            else
            {
                unit.ReceiveDamage();
                Debug.Log("MoveMonsterDamage");
            }
        }
      
    }
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * (-0.5F) + transform.right * direction.x * 0.6F, 0.05F);
        if (colliders.Length > 0 && colliders.All(x=> !x.GetComponent<CharSCR>())) direction *= -1.0F;
        sprite.flipX = direction.x > 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    
}
