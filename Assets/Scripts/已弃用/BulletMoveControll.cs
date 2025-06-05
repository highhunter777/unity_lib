using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class BulletMoveControll : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 _direction;
    private Vector2 _direction_last;
    private GameObject enemy;
 
    private bool havedEnemy=false;
    public int attackRange = 5;

   

    private Vector2 MoveDirection(Transform tar)
    {
        var direction = new Vector2(1, 0);

        if (tar != null)
        {

            direction=tar.position - transform.position;
            direction.Normalize();
        }


        return direction;

    }

   
    //获取目标
    public void SetTarget(GameObject obj)
    {
         
          enemy=obj;
          havedEnemy=true;
    }

    private bool CheckOverBorder()
    {
        bool isOver = false;
        var pos = gameObject.transform.position;
        if (pos.x > 15 || pos.x < -15)
        {
            isOver = true;
        }
        if (pos.y > 10 || pos.y < -10)
        {
            isOver = true;
        }

        return isOver;
    }



    private void FixedUpdate()
    {

      
        if (havedEnemy && enemy!=null)
        {
            _direction = MoveDirection(enemy.transform);
            if (_direction != null)
            {
                _direction_last = _direction;
            }
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
            var targetPos = (Vector2)transform.position + _direction;
            rb.DOMove(targetPos, speed).SetSpeedBased();
       
         }

        if (havedEnemy && enemy == null)
        {

         
            _direction = _direction_last;

            if (_direction[0]==0&& _direction[1] == 0)
            {
                _direction = new Vector2(1, 0);

            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
            var targetPos = (Vector2)transform.position + _direction;
            rb.DOMove(targetPos, speed).SetSpeedBased();

        }
        if (CheckOverBorder())
        {
            Destroy(gameObject);

        }
        

    }

 
}
