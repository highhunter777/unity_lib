using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{

    [SerializeField] private string targetTag;
    [SerializeField] private UnityEvent attacked;
    private bool isAttack=true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DealDamage(collision);
    }


    private void DealDamage(Collider2D collision)
    {
        if (!isAttack)
            return;

        if (collision.CompareTag(targetTag))
        {
            var damageable = collision.GetComponent<Damageable>();
            damageable.TakeDamage(1);
            TimersManager.SetTimer(this, 0.5f, CanAttack);
            isAttack = false;
            attacked.Invoke();
        }

    }

    private void CanAttack()
    {
        isAttack = true;
    }
}
