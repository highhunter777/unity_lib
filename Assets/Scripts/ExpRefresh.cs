using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpRefresh : MonoBehaviour
{
    [SerializeField] private Wave wave;
    [SerializeField] private int RefreshRequireExpNumber;//刷新所需经验点数量
    [SerializeField] private TMP_Text RefreshRequireExpNumberTextInStore;//商店刷新所需经验点数量
    [SerializeField] private TMP_Text RefreshRequireExpNumberText;//进化刷新所需经验点数量
    [SerializeField] private int havedRefreshCount=0;
    [SerializeField] private LevelManager  levelManager;

    private int ComputeRefreshRequireExp(int waveNumber,int havedRefreshCount)//更新当前刷新所需经验
    {
        float RequireExp =waveNumber+(havedRefreshCount-1)*0.5f*waveNumber ;

        return (int)RequireExp;
    }

    public void LevelUIUpdate()
    {
      
        RefreshRequireExpNumberText.text = RefreshRequireExpNumber.ToString();
        RefreshRequireExpNumberTextInStore.text = RefreshRequireExpNumber.ToString();
    }

    public void StoreUIUpdate()
    {

        RefreshRequireExpNumberText.text = RefreshRequireExpNumber.ToString();
    }


    public void Refresh()
    {

        if (levelManager.GetExpNumber() < RefreshRequireExpNumber)
        {
            //可设置几秒的提示
            return;
        }
        levelManager.HandleExpNumber(-RefreshRequireExpNumber);
        havedRefreshCount++;
        RefreshRequireExpNumber= ComputeRefreshRequireExp(wave.waveNumber, havedRefreshCount);
        LevelUIUpdate();
        levelManager.LevelUIUpdate();

    
    }

    private void Awake()
    {
        RefreshRequireExpNumber = ComputeRefreshRequireExp(wave.waveNumber,havedRefreshCount);
        LevelUIUpdate();
    }

    
}
