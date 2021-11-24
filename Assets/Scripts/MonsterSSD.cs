using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterSSD : Monster
{
    [SerializeField]
    private float speed = 0.5F;
    private Bullet bullet;
    private SpriteRenderer sprite;
    private Vector3 direction;
    private CharSCR pers;



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
                unit.ReceiveDamage();
                Debug.Log("MonsterSSDDamage");
        }
        
    }
    private void Move()
    {
        pers = FindObjectOfType<CharSCR>();
        if (gameObject.transform.position.x > pers.transform.position.x && Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) < 5 && Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) < 1)
        {
            speed = 4.0F;
            direction = -transform.right;
        }
        if (gameObject.transform.position.x < pers.transform.position.x && Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) < 5 && Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) < 1)
        {
            speed = 4.0F;
            direction = transform.right;
        }
        if (Mathf.Abs(gameObject.transform.position.x - pers.transform.position.x) > 5 || Mathf.Abs(gameObject.transform.position.y - pers.transform.position.y) > 1) speed = 0.5F;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * (-0.5F) + transform.right * direction.x * 0.6F, 0.05F);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<CharSCR>())) direction *= -1.0F;
        sprite.flipX = direction.x < 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
