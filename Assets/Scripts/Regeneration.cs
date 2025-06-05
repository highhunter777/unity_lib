using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [SerializeField] private Property playerProperty;
    [SerializeField] private Health playerHealth;
    private float Interval=1;
    



    private void Update()
    {
        Interval -= Time.deltaTime;

        if (Interval <= 0) {
        playerHealth.InCreaseHealth(playerHealth.Value);

            Interval = 1;
        }


    }



}
