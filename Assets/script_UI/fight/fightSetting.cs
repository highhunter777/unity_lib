using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightSetting : MonoBehaviour
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
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("PauseUI").transform.GetChild(0).gameObject;
        gameSettingUI.SetActive(true);
        GameObject menu = GameObject.FindGameObjectWithTag("Field").transform.GetChild(0).gameObject;
        menu.SetActive(false);
    }
}
