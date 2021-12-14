using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMeelee : MonoBehaviour
{
    private float timeBtwAtack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    //public Animator anim;
    public void Update()
    {
        if (timeBtwAtack <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //anim.SetTrigger("attack");
                Debug.Log(" нопка нажалась");
                OnAttack();
                
            }
            timeBtwAtack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAtack -= Time.deltaTime;
        }
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
}
