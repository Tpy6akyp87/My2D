using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitC : MonoBehaviour
{
    
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private Transform unitTransform;
    public Transform UnitTransform
    {
        get { return unitTransform; }
        set { unitTransform = value; }
    }


    public virtual void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);
    }
    public virtual void Jump() { }
    public virtual void Shoot() { }
    public virtual void ReceiveDamage() { }
    public virtual void Die() { }

}
