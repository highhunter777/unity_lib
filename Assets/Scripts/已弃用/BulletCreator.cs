using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform playerTransform;
    private GameObject target;

    public void CreateBullet()
    {
      
        var bullet=  Instantiate(bulletPrefab, playerTransform.position, Quaternion.identity);
        BulletMoveControll moveControll =bullet.GetComponent<BulletMoveControll>();
        moveControll.SetTarget(this.target);
       
    }


    public void SetTarget(GameObject obj)
    {
        this.target = obj;

    }
}
