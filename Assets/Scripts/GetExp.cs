using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetExp : MonoBehaviour
{

    private float detectionRadius;
    public LayerMask ExpLayer;
    [SerializeField] private Property playerProperty;
    private Transform currentTarget;


    private void Awake()
    {
        detectionRadius = playerProperty.GetExpRange;

    }

    // ��������ľ�����
    private void FindNearestTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, ExpLayer);

        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D col in hitColliders)
        {
           

            if (!col.gameObject.CompareTag("Exp"))
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

    private void Update()
    {
        // ��ⷶΧ�ڵľ�����
        FindNearestTarget();
        if(currentTarget != null)
        {
            //��������ɻ�ȡ��(����������������)
            var exp_script = currentTarget.gameObject.GetComponent<Exp>();
            exp_script.isBeGet = true;

        }
       
    }

}
