using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Field()
    {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).gameObject;
        gameSettingUI.SetActive(true);
        
        GameObject PauseUI = GameObject.FindGameObjectWithTag("PauseUI").transform.GetChild(0).gameObject;
        GlobalSettings.lastClosedObject = PauseUI;
        PauseUI.SetActive(false);
    }
}
