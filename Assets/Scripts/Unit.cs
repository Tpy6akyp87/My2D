using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void ReceiveDamage()
    {
        Die();
        Debug.Log("смерть тут пришла");
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
  