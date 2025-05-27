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
            
    }


}
