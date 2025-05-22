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
        GameObject menu = GameObject.FindGameObjectWithTag("menu").transform.GetChild(0).gameObject;
        menu.SetActive(true);
    }
}
