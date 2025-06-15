using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnScript : MonoBehaviour
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
        gameSettingUI.SetActive(false);

        if (GlobalSettings.lastClosedObject != null)
        {
            // 恢复物体状态
            GlobalSettings.lastClosedObject.SetActive(true);
        }
    }
}
