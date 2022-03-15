using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private CharSCR pers;
    public float jumpDist;
    public float jumpForce;

    public void Awake()
    {
        pers = FindObjectOfType<CharSCR>();
    }
    private void Jump()
    {
        if ((pers.transform.position - transform.position).magnitude < jumpDist && pers.transform.position.y > transform.position.y && Mathf.Abs(pers.transform.position.x - transform.position.x)<=0.4)
        {
            pers.rigidbody.velocity = new Vector3(0, jumpForce, 0);
        }
    }
}
