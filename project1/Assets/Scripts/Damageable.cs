using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color defaultColor;
    public void TakeDamage(int damage)
    {
        health.DecreaseHealth(damage);
        spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2,LoopType.Yoyo).ChangeStartValue(defaultColor);
    }

    private void Awake()
    {
        defaultColor = spriteRenderer.color;
    }
}
