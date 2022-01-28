using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonster : MonoBehaviour
{
    public GameObject addableMonster;
    //private MonsterShoot addMonster;
    public Transform summPosition;
    private bool summoned = false;
    private void Awake()
    {
        string monstername = addableMonster.name.ToString();
        addableMonster = Resources.Load<GameObject>(monstername);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && summoned == false)
        {
            summoned = true;
            GameObject gameObject = Instantiate(addableMonster, summPosition.position, addableMonster.transform.rotation) as GameObject;
        }
    }
}
