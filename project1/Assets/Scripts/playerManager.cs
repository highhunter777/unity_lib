using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public Vector2 Position
    {
        get { return (Vector2)playerTransform.position; }
    }
}
