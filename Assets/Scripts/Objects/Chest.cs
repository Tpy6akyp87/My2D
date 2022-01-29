using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Monster
{
    
    public GameObject addableObject;
    private bool summoned = false;
    private void Awake()
    {
        string objectName = addableObject.name.ToString();
        addableObject = Resources.Load<GameObject>(objectName);
    }
    public override void ReceiveDamage()
    {
        Debug.Log("Сундук сломали");
        summoned = true;
        GameObject gameObject = Instantiate(addableObject, transform.position + new Vector3(0,0.5f,0), addableObject.transform.rotation) as GameObject;
        Die();
    }

}
