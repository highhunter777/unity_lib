using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject LevelUpMenu;
    [SerializeField] private GameObject StoreMenu;
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private LevelManager levelManager;

    [Header("生成设置")]
    public GameObject[] monsterPrefabs;  // 怪物Prefab数组
    public float spawnInterval = 3f;     // 每波生成间隔
    public int maxMonsters = 10;         // 当前场上最大怪物数量
    public int totalMosters =4;     //当前关卡最多可以出现的怪物数量
    public int havedKillMonsters = 0;   //已经击杀的怪物数量

    public int maxSpawnPerInterval = 4; // 每次间隔最多生成数量
    public float minMonsterSpawnInterval=0.5f;//最小怪物生成间隔

    public float HavedeTime =20;    //关卡剩余时间

    public bool useColliderBounds = true; // 是否使用Collider范围

    [Header("自定义范围（当useColliderBounds为false时生效）")]
    public Vector2 spawnAreaCenter = Vector2.zero;//生成区域中心
    public Vector2 spawnAreaSize = new Vector2(5, 5);//生成区域大小

    private BoxCollider2D spawnArea;//生成区域
    private int currentMonsters = 0;//当前怪物数量

    [SerializeField] private UnityEvent WaveEnd;
 
    public bool canPause = true;

    void Start()
    {
        if (useColliderBounds)
        {
            spawnArea = GetComponent<BoxCollider2D>();
            if (spawnArea == null)
            {
                Debug.LogError("需要BoxCollider2D来定义生成区域！");
                return;
            }
        }

        StartCoroutine(SpawnRoutine());//启动协程
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);//每波等待的时间
            int spawnedThisBatch = 0;//本次生成的小批次的怪物数量
         
            while (currentMonsters < maxMonsters &&
                  monsterPrefabs.Length > 0 &&
                  spawnedThisBatch < maxSpawnPerInterval&&
                  canPause==true)
            {
                SpawnMonster();
                spawnedThisBatch++;

                // 单个生成间隔控制
                if (spawnedThisBatch < maxSpawnPerInterval)
                {
                    yield return new WaitForSeconds(minMonsterSpawnInterval);
                }
            }
        }
    }

    void SpawnMonster()
    {
        // 获取随机生成位置
        Vector2 spawnPosition = GetRandomPositionInArea();

        // 随机选择怪物类型
        int monsterIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject newMonster = Instantiate(
            monsterPrefabs[monsterIndex],
            spawnPosition,
            Quaternion.identity
        );

      

        currentMonsters++;

      ////  注册怪物死亡事件
       var monster = newMonster.GetComponent<Death>(); // 假设有Monster脚本
        if (monster != null)
        {
            monster.OnDeath += HandleMonsterDeath;
        
        
        }
    }

    Vector2 GetRandomPositionInArea()
    {
        if (useColliderBounds && spawnArea != null)
        {
            return new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );
        }
        else
        {
            return spawnAreaCenter + new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );
        }
    }

    void HandleMonsterDeath()
    {
        currentMonsters--;
        totalMosters--;  
        havedKillMonsters++;  
    }
   public void WaveReset()
    {
        HavedeTime = 20;
        totalMosters = 20;
        currentMonsters= 0;
    }

    private void Update()
    {

        //计时
        if (canPause)
        {
            HavedeTime -= Time.deltaTime;
            tmpText.text = ((int)HavedeTime).ToString();
        }

        //关卡结束
        if (totalMosters <= 0 || HavedeTime <= 0)
        {
            if (canPause)
            {
                if (levelManager.EvolutionCount > 0)
                {
                    LevelUpMenu.SetActive(true);
                }
                else
                {
                    StoreMenu.SetActive(true);

                }


                WaveEnd.Invoke();
             
                canPause = false;
            }
        }
    }

}
