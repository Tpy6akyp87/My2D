using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Heart heart;
    private Patron patron;
    private int rnd;
    public virtual void ReceiveDamage()
    {
        Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        rnd = Random.Range(1,5);
        heart = Resources.Load<Heart>("Heart");
        patron = Resources.Load<Patron>("Patron");
        if (rnd == 1)
        {
            Heart newheart = Instantiate(heart, transform.position + new Vector3(0, 0.5f, 0), heart.transform.rotation) as Heart;
        }
        if (rnd == 2)
        {
            Patron newpatron = Instantiate(patron, transform.position + new Vector3(0, 0.5f, 0), patron.transform.rotation) as Patron;
        }
    }
}
  