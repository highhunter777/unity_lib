using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private UnityEvent<float> healthChanged;

    public float MaxHp;

    // ��������
    private Queue<float> healthQueue = new();

    public float Value
    {
        get { return health; }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void DecreaseHealth(float damage)
    { 
        health -= damage;

       
        healthQueue.Enqueue(health);
        
     
       
       
    }


    public void InCreaseHealth(float blood) {


        health += blood;

        if (health >= MaxHp)
        {
            health = MaxHp;
        }

        healthQueue.Enqueue(health);
        
       

    }

   private void Update()
    {
        if (healthQueue.Count > 0)
        {
            float firstHealth = healthQueue.Peek(); // ���ص�һ��Ѫ������ֵ
            healthChanged.Invoke(firstHealth);
            healthQueue.Dequeue();
        }



    }


}
