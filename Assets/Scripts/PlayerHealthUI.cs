using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image hpImage;//血条UI
    [SerializeField] private Health health;
    [SerializeField] private TMP_Text HpValue;//血条数值UI
 


    private void Start()
    {
       
        string hpValue= ((int)health.Value).ToString() + '/' + ((int)health.MaxHp).ToString();
        HpValue.text = hpValue;
    }

    public void UpdateUI()
    {

        string hpValue = ((int)health.Value).ToString() + '/' +((int)health.MaxHp).ToString();
        HpValue.text = hpValue;
        
        hpImage.fillAmount =(float)( health.Value) / (float)(health.MaxHp);
       
    }
}
