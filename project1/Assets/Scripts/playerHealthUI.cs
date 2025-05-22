using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthUI : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] private Health health;
    public float MaxHp;
    public void UpdateUI()
    {
        hpImage.fillAmount = health.Value / MaxHp;
    }
}
