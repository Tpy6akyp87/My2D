using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronBar : MonoBehaviour
{
    private Transform[] patrons = new Transform[6];

    private CharSCR character;
    private void Awake()
    {
        character = FindObjectOfType<CharSCR>();

        for (int i = 0; i < patrons.Length; i++)
        {
            patrons[i] = transform.GetChild(i);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < patrons.Length; i++)
        {
            if (i < character.Patrons) patrons[i].gameObject.SetActive(true);
            else patrons[i].gameObject.SetActive(false);
        }
    }
}
