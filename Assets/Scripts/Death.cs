using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{

    [SerializeField] private UnityEvent died;
    public Action OnDeath;
    [SerializeField] private GameObject ExpObj;

    public void CheckDeath(float health)
    { 
         if (health <= 0)
        {
          
            Die();
          
        }
    
    }


    public void Die()
    {
        //实例化经验球
        Instantiate(ExpObj, transform.position, Quaternion.identity);
       
        OnDeath.Invoke();
        died.Invoke();
        Destroy(gameObject);
    }
}
