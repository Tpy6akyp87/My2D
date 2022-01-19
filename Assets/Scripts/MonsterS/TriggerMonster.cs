using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonster : MonoBehaviour
{
    private MonsterShoot addMonster;
    public Transform summPosition;
    private void Awake()
    {
        addMonster = Resources.Load<MonsterShoot>("MonsterShoot");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MonsterShoot newMonsterShoot = Instantiate(addMonster, summPosition.position, addMonster.transform.rotation) as MonsterShoot;
        }
    }
}
