using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class EnemyMoveControll : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rb;

    //[SerializeField] private float speed;


    //private void FixedUpdate()
    //{
    //    var pos = (Vector2)transform.position;
    //    //计算方向向量
    //    var playerDirection = PlayerManager.Position - pos;
    //    playerDirection.Normalize();
    //    //更新方向
    //    var tar = pos + playerDirection;

    //    rb.DOMove(tar, speed).SetSpeedBased();
    //}
    [Header("Movement Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    [Header("Separation Settings")]
    [SerializeField] private float separationRadius = 1.5f;  // 检测其他敌人的半径
    [SerializeField] private float separationForce = 2f;    // 排斥力强度
    [SerializeField] private LayerMask enemyLayer;          // 敌人所在的层级

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        // 1. 计算朝向玩家的基础移动方向
        Vector2 playerDirection = (PlayerManager.Position - pos).normalized;
        Vector2 targetVelocity = playerDirection * speed;

        // 2. 计算分离向量（避免重叠）
        Vector2 separation = CalculateSeparation();

        // 3. 结合基础移动和分离向量
        Vector2 finalVelocity = targetVelocity + separation;

        // 4. 应用速度
        rb.velocity = finalVelocity;
    }

    private Vector2 CalculateSeparation()
    {
        Vector2 separationForceVector = Vector2.zero;

        // 检测范围内的所有敌人
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(
            transform.position,
            separationRadius,
            enemyLayer
        );

        foreach (var enemy in nearbyEnemies)
        {
            if (enemy.gameObject == gameObject) continue; // 跳过自己

            // 计算排斥方向：从其他敌人指向自己
            Vector2 directionToMe = transform.position - enemy.transform.position;

            // 距离越近排斥力越大（使用平方反比衰减）
            float distance = directionToMe.magnitude;
            if (distance > 0)
            {
                float forceMultiplier = Mathf.Clamp01(1 - distance / separationRadius);
                separationForceVector += directionToMe.normalized * (separationForce * forceMultiplier);
            }
        }

        return separationForceVector;
    }

}
