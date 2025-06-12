using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    public static BGMScript BGM;  // 单例实例
    private AudioSource audioSource;    // 音频源组件

    void Awake()
    {
        // 如果 Instance 为 null，则设置它为当前的 BGMScript 实例，否则销毁当前物体
        if (BGM == null)
        {
            BGM = this;
            DontDestroyOnLoad(this.gameObject);  // 确保这个物体在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 获取音频源组件
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
