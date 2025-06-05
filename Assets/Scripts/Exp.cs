using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Exp : MonoBehaviour
{

    public float speed = 20f;
    public bool isBeGet=false;
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject manager;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        manager = GameObject.FindWithTag("Manager");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isBeGet)
        {

            BeGet();

        }
    }

    private void BeGet()
    {
        
        Vector3 direction = (player.transform.position - transform.position).normalized;
        // œÚ«∞“∆∂Ø
        if (rb != null && direction != null)
        {
            rb.velocity = direction * speed;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)

    {



        Deal(collision);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        Deal(collision);

    }


    private void Deal(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
           var levelManager=manager.GetComponent<LevelManager>();

            levelManager.HandleMonsterDeath();

           Destroy(gameObject);
        }

    }
}
