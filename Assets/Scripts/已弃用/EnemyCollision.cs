using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCollision : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Collision Avoidance")]
    [SerializeField] private float avoidanceRadius = 1.5f;
    [SerializeField] private float avoidanceForce = 8f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D enemyCollider;

    private Transform player;
    private List<Collider2D> nearbyEnemies = new List<Collider2D>();
    private ContactFilter2D contactFilter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 设置接触过滤器
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(enemyLayer);
        contactFilter.useLayerMask = true;

        // 确保有必要的组件
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (enemyCollider == null) enemyCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 moveDirection = CalculateMovementDirection();
        ApplyMovement(moveDirection);
        RotateTowardsPlayer();
    }

    private Vector2 CalculateMovementDirection()
    {
        Vector2 toPlayer = (player.position - transform.position).normalized;
        Vector2 avoidance = CalculateAvoidanceForce();

        // 结合玩家方向和排斥力
        Vector2 combinedDirection = (toPlayer + avoidance).normalized;

        return combinedDirection;
    }

    private Vector2 CalculateAvoidanceForce()
    {
        Vector2 avoidanceVector = Vector2.zero;

        // 获取附近敌人
        int count = Physics2D.OverlapCircle(transform.position, avoidanceRadius, contactFilter, nearbyEnemies);

        if (count > 0)
        {
            foreach (var enemy in nearbyEnemies)
            {
                if (enemy.gameObject == gameObject) continue;

                Vector2 toOther = transform.position - enemy.transform.position;
                float distance = toOther.magnitude;

                // 距离越近排斥力越大
                float forceMultiplier = Mathf.Clamp01(1 - (distance / avoidanceRadius));
                avoidanceVector += toOther.normalized * forceMultiplier;
            }
        }

        return avoidanceVector * avoidanceForce;
    }

    private void ApplyMovement(Vector2 direction)
    {
        // 使用物理移动而不是DOTween
        rb.velocity = direction * moveSpeed;
    }

    private void RotateTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
    }
}