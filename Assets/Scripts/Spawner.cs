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

    [Header("��������")]
    public GameObject[] monsterPrefabs;  // ����Prefab����
    public float spawnInterval = 3f;     // ÿ�����ɼ��
    public int maxMonsters = 10;         // ��ǰ��������������
    public int totalMosters =4;     //��ǰ�ؿ������Գ��ֵĹ�������
    public int havedKillMonsters = 0;   //�Ѿ���ɱ�Ĺ�������

    public int maxSpawnPerInterval = 4; // ÿ�μ�������������
    public float minMonsterSpawnInterval=0.5f;//��С�������ɼ��

    public float HavedeTime =20;    //�ؿ�ʣ��ʱ��

    public bool useColliderBounds = true; // �Ƿ�ʹ��Collider��Χ

    [Header("�Զ��巶Χ����useColliderBoundsΪfalseʱ��Ч��")]
    public Vector2 spawnAreaCenter = Vector2.zero;//������������
    public Vector2 spawnAreaSize = new Vector2(5, 5);//���������С

    private BoxCollider2D spawnArea;//��������
    private int currentMonsters = 0;//��ǰ��������

    [SerializeField] private UnityEvent WaveEnd;
 
    public bool canPause = true;

    void Start()
    {
        if (useColliderBounds)
        {
            spawnArea = GetComponent<BoxCollider2D>();
            if (spawnArea == null)
            {
                Debug.LogError("��ҪBoxCollider2D��������������");
                return;
            }
        }

        StartCoroutine(SpawnRoutine());//����Э��
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);//ÿ���ȴ���ʱ��
            int spawnedThisBatch = 0;//�������ɵ�С���εĹ�������
         
            while (currentMonsters < maxMonsters &&
                  monsterPrefabs.Length > 0 &&
                  spawnedThisBatch < maxSpawnPerInterval&&
                  canPause==true)
            {
                SpawnMonster();
                spawnedThisBatch++;

                // �������ɼ������
                if (spawnedThisBatch < maxSpawnPerInterval)
                {
                    yield return new WaitForSeconds(minMonsterSpawnInterval);
                }
            }
        }
    }

    void SpawnMonster()
    {
        // ��ȡ�������λ��
        Vector2 spawnPosition = GetRandomPositionInArea();

        // ���ѡ���������
        int monsterIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject newMonster = Instantiate(
            monsterPrefabs[monsterIndex],
            spawnPosition,
            Quaternion.identity
        );

      

        currentMonsters++;

      ////  ע����������¼�
       var monster = newMonster.GetComponent<Death>(); // ������Monster�ű�
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

        //��ʱ
        if (canPause)
        {
            HavedeTime -= Time.deltaTime;
            tmpText.text = ((int)HavedeTime).ToString();
        }

        //�ؿ�����
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
