using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalSettings : MonoBehaviour
{
    // 定义静态变量，所有文件都可以访问这个变量
    public static GameObject lastClosedObject;
}

public class settingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void settingUI()
    {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).gameObject;
        gameSettingUI.SetActive(true);

        GameObject menu = GameObject.FindGameObjectWithTag("menu").transform.GetChild(0).gameObject;
        // 关闭物体时记录
        GlobalSettings.lastClosedObject = menu;
        menu.SetActive(false);
    }
}


