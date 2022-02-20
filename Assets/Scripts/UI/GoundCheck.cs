using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    public bool isGrounded = true;
    private void OnTriggerStay2D(Collider2D collider)
    {
        isGrounded = collider != null && (((1 << collider.gameObject.layer) & groundLayerMask) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
    private void Update()
    {
        if (isGrounded)
            Debug.Log("Стою");
        else
            Debug.Log("Не стою");
    }
}