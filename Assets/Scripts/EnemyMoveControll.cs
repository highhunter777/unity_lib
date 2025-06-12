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
    //    //���㷽������
    //    var playerDirection = PlayerManager.Position - pos;
    //    playerDirection.Normalize();
    //    //���·���
    //    var tar = pos + playerDirection;

    //    rb.DOMove(tar, speed).SetSpeedBased();
    //}
    [Header("Movement Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    [Header("Separation Settings")]
    [SerializeField] private float separationRadius = 1.5f;  // ����������˵İ뾶
    [SerializeField] private float separationForce = 2f;    // �ų���ǿ��
    [SerializeField] private LayerMask enemyLayer;          // �������ڵĲ㼶

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        // 1. ���㳯����ҵĻ����ƶ�����
        Vector2 playerDirection = (PlayerManager.Position - pos).normalized;
        Vector2 targetVelocity = playerDirection * speed;

        // 2. ������������������ص���
        Vector2 separation = CalculateSeparation();

        // 3. ��ϻ����ƶ��ͷ�������
        Vector2 finalVelocity = targetVelocity + separation;

        // 4. Ӧ���ٶ�
        rb.velocity = finalVelocity;
    }

    private Vector2 CalculateSeparation()
    {
        Vector2 separationForceVector = Vector2.zero;

        // ��ⷶΧ�ڵ����е���
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(
            transform.position,
            separationRadius,
            enemyLayer
        );

        foreach (var enemy in nearbyEnemies)
        {
            if (enemy.gameObject == gameObject) continue; // �����Լ�

            // �����ųⷽ�򣺴���������ָ���Լ�
            Vector2 directionToMe = transform.position - enemy.transform.position;

            // ����Խ���ų���Խ��ʹ��ƽ������˥����
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
