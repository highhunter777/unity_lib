using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private static PlayerManager instance;
    public static Vector2 Position
    {
        get { return (Vector2)instance.playerTransform.position; }
    }
    private void Awake()
    {
        instance = this;
    }
}
