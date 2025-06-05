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

        
        // ��ʼ״̬���������
        enemyCollider = GetComponent<CircleCollider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyMoveControll = GetComponent<EnemyMoveControll>();
        enemyCollider.enabled = false;
        enemyRigidbody.isKinematic = true;
        enemyMoveControll.enabled = false;


        spriteRenderer = GetComponent<SpriteRenderer>();

        Sprite loadedSprite = Resources.Load<Sprite>("Sprites/a"); // ���ļ���׺��

        if (spriteRenderer != null && loadedSprite != null)
        {
            spriteRenderer.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer�������δ��ֵ��");
        }

       

    }



    public void OnAppearAnimationEnd()
    {
        GetComponent<Animator>().enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        

        // ����������л�״̬
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
            Debug.LogError("SpriteRenderer�������δ��ֵ��");
        }


    }



    public bool GetCanBeLocate()
    {

        return canBeLocate;
    }
}
