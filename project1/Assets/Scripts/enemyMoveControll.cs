using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class enemyMoveControll : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;

    [SerializeField] private playerManager playerManager;
  
    private void FixedUpdate()
    {
        var pos = (Vector2)transform.position;
        //计算方向向量
        var playerDirection = playerManager.Position - pos;
        playerDirection.Normalize();
        //更新方向
        var tar = pos + playerDirection;
       
        rb.DOMove(tar, speed).SetSpeedBased();
    }
}
