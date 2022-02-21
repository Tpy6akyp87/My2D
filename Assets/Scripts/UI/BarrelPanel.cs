using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPanel : MonoBehaviour
{
    private Transform[] barrels = new Transform[3];

    private CharSCR character;
    private void Awake()
    {
        character = FindObjectOfType<CharSCR>();

        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i] = transform.GetChild(i);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            if (i < character.Barrels) barrels[i].gameObject.SetActive(true);
            else barrels[i].gameObject.SetActive(false);
        }
    }
}
