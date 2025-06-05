using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLocateEnemy : MonoBehaviour
{

    [SerializeField] private UnityEvent<GameObject> enemyEmerged;
    private GameObject lastDetectedEnemy = null; // 记录上一次检测到的敌人
    public int attackRange = 10;

    private GameObject LocateEnemy()
    {
        var results = new Collider2D[5];//用于储存Collider2D(用一个圆形范围的Collider去检测周围敌人)的数组
        //transform.position为子弹位置，5为圆形半径，results为结果数组
        Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, results);
        foreach (var result in results)
        {
            if (result != null && result.CompareTag("Enemy"))
            {
                return result.gameObject;

            }

        }
        return null;
    }



    private void FixedUpdate()
    {
        var currentEnemy = LocateEnemy();

        // 如果当前检测到敌人，且上一次没检测到（即敌人刚进入范围）
        if (currentEnemy != null &&currentEnemy.GetComponent<EnemyEmerge>().GetCanBeLocate()&& lastDetectedEnemy == null)
        {
            enemyEmerged.Invoke(currentEnemy); // 触发事件
           
        }

        lastDetectedEnemy = currentEnemy; // 更新记录
    }
}
