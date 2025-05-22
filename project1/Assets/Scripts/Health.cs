using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] private UnityEvent healthChanged;
    public int Value
    {
        get { return health; }
    }

    public void DecreaseHealth(int amount)
    { 
        health-=amount;
        healthChanged.Invoke();
    }

}
