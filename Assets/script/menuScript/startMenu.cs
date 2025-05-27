using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameSettingUI = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).gameObject;
        gameSettingUI.SetActive(false);
        GameObject musicUI = GameObject.FindGameObjectWithTag("musicUI").transform.GetChild(0).gameObject;
        musicUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
