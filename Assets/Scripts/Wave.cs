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
        //关闭玩家
        player.SetActive(false);
        property.isLaunch = false;

        //重置波次属性
       spawner.WaveReset();
        

        //清理敌人和子弹
        List<GameObject> clones = GetAllClones();
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }
    }

  
    public void LevelMenuExit()
    {
        

        //关闭升级菜单
        LevelUpMenu.SetActive(false);
        //启动商店菜单
       StoreMenu.SetActive(true);
    }
    public void StoreMenuExit()
    {
        //更新波数
        waveNumber++;
        waveNumberText.text = waveNumber.ToString();

        //启动玩家
        player.SetActive(true);
        property.isLaunch = true;
        //启动spawner的可暂停属性
        spawner.canPause = true;

        //关闭商店菜单
        StoreMenu.SetActive(false);
    }
    public List<GameObject> GetAllClones()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> clones = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 检测名称是否以 "(Clone)" 结尾
            if (obj.name.Contains("(Clone)"))
            {
                clones.Add(obj);
            }
        }
        return clones;
    }
}
