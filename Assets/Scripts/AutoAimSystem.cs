using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AutoAimSystem : MonoBehaviour
{
    [Header("��������")]
    public Transform weaponPivot; // ������ת���ĵ�
    public GameObject projectilePrefab; // �ӵ�Ԥ����
    public Transform firePoint; // �ӵ�����λ��

    [Header("��׼����")]
    public LayerMask enemyLayer; // ���˲㼶
    public float aimSpeed = 10f; // ��׼�ٶ�
    [Header("������Χ")]
    public float detectionRadius = 10f; // ���뾶
   
    [Header("�����ٶ�")]
    public float attackRate=2;//�����ٶ�(��/��)

    private Transform currentTarget; // ��ǰĿ��
    private float nextFireTime; // �´����ʱ��
    private bool isTargetLocked; // �Ƿ�������Ŀ��

    private void Update()
    {
        // ��ⷶΧ�ڵĵ���
        FindNearestTarget();

        // �����Ŀ�꣬��ת��������׼
        if (currentTarget != null)
        {
            AimAtTarget();

            // ����߼�
            if (Time.time >= nextFireTime)
            {
                Fire();
                float fireRate = 1 / attackRate; // ���Ƶ��(��/��)
                nextFireTime = Time.time + fireRate;
            }


            isTargetLocked = true;
        }
        else
        {
            // û��Ŀ��ʱ����״̬
            if (isTargetLocked)
            {
                ResetWeaponRotation();
                //if (aimLine != null) aimLine.enabled = false;
                isTargetLocked = false;
            }
        }
    }

    // ��������ĵ���
    private void FindNearestTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D col in hitColliders)
        {
            if (!col.isTrigger)
            { 
                continue;
            }

            if (col.gameObject.CompareTag("Enemy")) 
            {
                if (!col.gameObject.GetComponent<EnemyEmerge>().GetCanBeLocate())

                { 
                    continue; 
                }
            
            }
            else
            {
                continue;
            }


            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = col.transform;
            }
        }

        currentTarget = nearestTarget;
    }

    // ��׼Ŀ��
    private void AimAtTarget()
    {
        if (currentTarget == null || weaponPivot == null) return;

        // ���㷽��
        Vector3 direction = (currentTarget.position - weaponPivot.position).normalized;

        // ����Ƕ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ƽ����ת����
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weaponPivot.rotation = Quaternion.Slerp(weaponPivot.rotation, targetRotation, aimSpeed * Time.deltaTime);
    }

    // �����ָ�Ĭ�ϳ���
    private void ResetWeaponRotation()
    {
        if (weaponPivot == null) return;

        // ƽ���ص�Ĭ�ϽǶ�
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
        weaponPivot.rotation = Quaternion.Slerp(weaponPivot.rotation, defaultRotation, aimSpeed * Time.deltaTime);
    }

    // �����ӵ�
    private void Fire()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // �����ӵ�
        var bullet=Instantiate(projectilePrefab);
        bullet.transform.position = firePoint.position;

        if (currentTarget == null ) return;

        // ���㷽��
        Vector3 direction = (currentTarget.position - firePoint.position).normalized;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
        bullet.GetComponent<Bullet>().Init(direction);
    }

    public void ApplyNewAttackSpeed(float newAttackSpeedBonus)
    {
        attackRate *= (1 + newAttackSpeedBonus * 0.01f);

    }
}



