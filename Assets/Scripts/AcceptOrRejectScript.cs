using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AcceptOrRejectScript : MonoBehaviour
{
    public bool isDeciding = false;
    public bool isAccepted = false;
    //public void Accept(InputAction.CallbackContext ctx)
    //{
    //    if(ctx.performed && isDeciding)
    //    {
    //        isDeciding = false;
    //        isAccepted = true;
    //        Debug.Log("Accepted");
    //        // Add your acceptance logic here
    //    }
    //}
    //public void Reject(InputAction.CallbackContext ctx)
    //{
    //    if (ctx.performed && isDeciding)
    //    {
    //        isDeciding = false;
    //        isAccepted = false;
    //        Debug.Log("Rejected");
    //        // Add your rejection logic here
    //    }
    //}
}
