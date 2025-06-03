using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmerge : MonoBehaviour
{
    [SerializeField] private GameObject EnemyAni;
    [SerializeField] private GameObject EmemySprite;

    private SpriteRenderer spriteRenderer;

    private CircleCollider2D enemyCollider ;
    private Rigidbody2D enemyRigidbody ;
    private EnemyMoveControll enemyMoveControll ;
    private bool canBeLocate=false;


    private void Awake()
    {

        
        // 初始状态：禁用组件
        enemyCollider = GetComponent<CircleCollider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyMoveControll = GetComponent<EnemyMoveControll>();
        enemyCollider.enabled = false;
        enemyRigidbody.isKinematic = true;
        enemyMoveControll.enabled = false;


        spriteRenderer = GetComponent<SpriteRenderer>();

        Sprite loadedSprite = Resources.Load<Sprite>("Sprites/a"); // 无文件后缀名

        if (spriteRenderer != null && loadedSprite != null)
        {
            spriteRenderer.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer组件或精灵未赋值！");
        }

       

    }



    public void OnAppearAnimationEnd()
    {
        GetComponent<Animator>().enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        

        // 启用组件或切换状态
        enemyCollider.enabled = true;
        enemyRigidbody.isKinematic = false;
        enemyMoveControll.enabled = true;
        canBeLocate = true;

        Sprite loadedSprite = EmemySprite.GetComponent<SpriteRenderer>().sprite;
        if (spriteRenderer != null && loadedSprite != null)
        {
            spriteRenderer.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer组件或精灵未赋值！");
        }


    }



    public bool GetCanBeLocate()
    {

        return canBeLocate;
    }
}
