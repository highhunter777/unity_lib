using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    private Property playerProperty;
    [SerializeField] private string targetTag;
    [SerializeField] private UnityEvent attacked;
    private bool isAttack=true;

    public float monsterDamage = 1;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerProperty = player.GetComponent<Property>();
    }


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


            monsterDamage = DealDefense(monsterDamage);


            if (DealEvasion())
            {
               monsterDamage = 0;

            }

           

            damageable.TakeDamage(monsterDamage);
            TimersManager.SetTimer(this, 0.5f, CanAttack);
            isAttack = false;
            attacked.Invoke();
        }

   

    }

    private void CanAttack()
    {
        isAttack = true;
    }

    private float DealCrit(float damage)
    {
        bool isCrit = UnityEngine.Random.Range(0f, 100f) <= playerProperty.CriticalRate;
        float newDamage;
        if (isCrit)
        {
            newDamage = damage * (1 + playerProperty.attackBonus * 0.01f) * playerProperty.CriticalValue * 0.01f;
        }
        else
        {
            newDamage = damage * (1 + playerProperty.attackBonus * 0.01f);
        }

        return newDamage;
    }

    private float DealDefense(float damage)
    {
        float newDamage=damage*(1-playerProperty.Defense * 0.01f);

        return newDamage;
    }



    private bool DealEvasion()
    {
        bool isEvasion = UnityEngine.Random.Range(0f, 100f) <= playerProperty.EvasionRate;


        return isEvasion;

    }
}
