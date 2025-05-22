using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMoveControll : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
   
    [SerializeField]private float speed;

    private Vector2 inputDirection;
    public void Move(InputAction.CallbackContext callBack)
    {
        inputDirection = callBack.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {    
        var pos=(Vector2)transform.position;
        var tar=inputDirection+pos;
        if (pos == tar)
        {
            return;
        }

        rb.DOMove(tar,speed).SetSpeedBased();
    }
}
