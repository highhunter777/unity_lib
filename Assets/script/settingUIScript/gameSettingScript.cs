using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameSettingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<gameSettingScript>().Length > 1)
        {
            Destroy(gameObject);  // 如果已有实例，销毁当前实例
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        // 在开始时调用 musicVolume() 来设置监听器
        musicVolume();
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    void musicVolume()
    {
        //获取音量滑动条
        Slider musicSlider = GameObject.FindGameObjectWithTag("gameSettingUI").transform.GetChild(0).GetChild(1).GetChild(1).gameObject.transform.GetComponent<Slider>();

        // 设置初始音量
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);  // 默认音量是1
        //Debug.Log("音量设置为：" + musicSlider.value+"跑到这里");
        //设置音量
        BGMScript.BGM.transform.GetComponent<AudioSource>().volume = musicSlider.value;
        Volume(musicSlider.value);  // 根据滑动条的值设置音量
         // 设置监听器，监听滑动条的值变化
        musicSlider.onValueChanged.AddListener(Volume);
    }

    // 滑动条的值变化时被调用
    void Volume(float value)
    {
        // 设置音量
        BGMScript.BGM.transform.GetComponent<AudioSource>().volume = value;
        // 保存音量
        PlayerPrefs.SetFloat("musicVolume", value);
        PlayerPrefs.Save();  // 强制保存 PlayerPrefs
        
    }
}
