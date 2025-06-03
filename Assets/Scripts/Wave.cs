using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Wave : MonoBehaviour
{
 
     public int waveNumber;

    [SerializeField] private GameObject player;
    [SerializeField] private Property property;
    [SerializeField] private GameObject LevelUpMenu;
    [SerializeField] private GameObject StoreMenu;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TMP_Text waveNumberText;
    //private bool infiniteWave=false;

    public void WaveUpdate()
    {
        //�ر����
        player.SetActive(false);
        property.isLaunch = false;

        //���ò�������
       spawner.WaveReset();
        

        //������˺��ӵ�
        List<GameObject> clones = GetAllClones();
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }
    }

  
    public void LevelMenuExit()
    {
        

        //�ر������˵�
        LevelUpMenu.SetActive(false);
        //�����̵�˵�
       StoreMenu.SetActive(true);
    }
    public void StoreMenuExit()
    {
        //���²���
        waveNumber++;
        waveNumberText.text = waveNumber.ToString();

        //�������
        player.SetActive(true);
        property.isLaunch = true;
        //����spawner�Ŀ���ͣ����
        spawner.canPause = true;

        //�ر��̵�˵�
        StoreMenu.SetActive(false);
    }
    public List<GameObject> GetAllClones()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> clones = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // ��������Ƿ��� "(Clone)" ��β
            if (obj.name.Contains("(Clone)"))
            {
                clones.Add(obj);
            }
        }
        return clones;
    }
}
