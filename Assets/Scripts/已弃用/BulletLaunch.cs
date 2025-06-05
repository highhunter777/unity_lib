using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    [SerializeField]private BulletCreator creator;
    [SerializeField] private Property property;

    private float interval = 1f;
    private void Launch()
    {
        if(property.isLaunch==true)
        creator.CreateBullet();
         
    }

    public void StartLaunch(GameObject obj)
    {

        
        creator.SetTarget(obj);
        TimersManager.SetLoopableTimer(this, interval, Launch);
      

    }
}
