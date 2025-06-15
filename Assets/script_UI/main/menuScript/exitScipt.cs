using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScipt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // 退出游戏
    public void QuitApplication()
    {
        // 如果是运行在编辑器中，则停止播放
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 如果是发布的版本，则退出应用程序
            Application.Quit();
        #endif
    }
}
