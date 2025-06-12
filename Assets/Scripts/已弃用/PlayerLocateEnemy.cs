using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLocateEnemy : MonoBehaviour
{

    [SerializeField] private UnityEvent<GameObject> enemyEmerged;
    private GameObject lastDetectedEnemy = null; // ��¼��һ�μ�⵽�ĵ���
    public int attackRange = 10;

    private GameObject LocateEnemy()
    {
        var results = new Collider2D[5];//���ڴ���Collider2D(��һ��Բ�η�Χ��Colliderȥ�����Χ����)������
        //transform.positionΪ�ӵ�λ�ã�5ΪԲ�ΰ뾶��resultsΪ�������
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

        // �����ǰ��⵽���ˣ�����һ��û��⵽�������˸ս��뷶Χ��
        if (currentEnemy != null &&currentEnemy.GetComponent<EnemyEmerge>().GetCanBeLocate()&& lastDetectedEnemy == null)
        {
            enemyEmerged.Invoke(currentEnemy); // �����¼�
           
        }

        lastDetectedEnemy = currentEnemy; // ���¼�¼
    }
}
