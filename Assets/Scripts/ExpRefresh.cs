using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpRefresh : MonoBehaviour
{
    [SerializeField] private Wave wave;
    [SerializeField] private int RefreshRequireExpNumber;//ˢ�����辭�������
    [SerializeField] private TMP_Text RefreshRequireExpNumberTextInStore;//�̵�ˢ�����辭�������
    [SerializeField] private TMP_Text RefreshRequireExpNumberText;//����ˢ�����辭�������
    [SerializeField] private int havedRefreshCount=0;
    [SerializeField] private LevelManager  levelManager;

    private int ComputeRefreshRequireExp(int waveNumber,int havedRefreshCount)//���µ�ǰˢ�����辭��
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
            //�����ü������ʾ
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
