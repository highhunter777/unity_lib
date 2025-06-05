using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AutoAimSystem : MonoBehaviour
{
    [Header("武器设置")]
    public Transform weaponPivot; // 武器旋转中心点
    public GameObject projectilePrefab; // 子弹预制体
    public Transform firePoint; // 子弹发射位置

    [Header("瞄准设置")]
    public LayerMask enemyLayer; // 敌人层级
    public float aimSpeed = 10f; // 瞄准速度
    [Header("攻击范围")]
    public float detectionRadius = 10f; // 检测半径
   
    [Header("攻击速度")]
    public float attackRate=2;//攻击速度(发/秒)

    private Transform currentTarget; // 当前目标
    private float nextFireTime; // 下次射击时间
    private bool isTargetLocked; // 是否已锁定目标

    private void Update()
    {
        // 检测范围内的敌人
        FindNearestTarget();

        // 如果有目标，旋转武器并瞄准
        if (currentTarget != null)
        {
            AimAtTarget();

            // 射击逻辑
            if (Time.time >= nextFireTime)
            {
                Fire();
                float fireRate = 1 / attackRate; // 射击频率(秒/发)
                nextFireTime = Time.time + fireRate;
            }


            isTargetLocked = true;
        }
        else
        {
            // 没有目标时重置状态
            if (isTargetLocked)
            {
                ResetWeaponRotation();
                //if (aimLine != null) aimLine.enabled = false;
                isTargetLocked = false;
            }
        }
    }

    // 查找最近的敌人
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

    // 瞄准目标
    private void AimAtTarget()
    {
        if (currentTarget == null || weaponPivot == null) return;

        // 计算方向
        Vector3 direction = (currentTarget.position - weaponPivot.position).normalized;

        // 计算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 平滑旋转武器
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weaponPivot.rotation = Quaternion.Slerp(weaponPivot.rotation, targetRotation, aimSpeed * Time.deltaTime);
    }

    // 武器恢复默认朝向
    private void ResetWeaponRotation()
    {
        if (weaponPivot == null) return;

        // 平滑回到默认角度
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
        weaponPivot.rotation = Quaternion.Slerp(weaponPivot.rotation, defaultRotation, aimSpeed * Time.deltaTime);
    }

    // 发射子弹
    private void Fire()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // 创建子弹
        var bullet=Instantiate(projectilePrefab);
        bullet.transform.position = firePoint.position;

        if (currentTarget == null ) return;

        // 计算方向
        Vector3 direction = (currentTarget.position - firePoint.position).normalized;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
        bullet.GetComponent<Bullet>().Init(direction);
    }

    public void ApplyNewAttackSpeed(float newAttackSpeedBonus)
    {
        attackRate *= (1 + newAttackSpeedBonus * 0.01f);

    }
}



