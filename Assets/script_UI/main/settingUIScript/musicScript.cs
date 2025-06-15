using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicScript : MonoBehaviour
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
        GameObject musicUI = GameObject.FindGameObjectWithTag("musicUI").transform.GetChild(0).gameObject;
        musicUI.SetActive(true);
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).gameObject;
        gameSettingUI.SetActive(false);
    }
}
