using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("经验数据部分")]
    [SerializeField] private int Level;//当前等级
    [SerializeField] private int ExpNumber;//经验点数量
    [SerializeField] private int LevelUpRequireExpNumber;//升级所需经验点数量
    [SerializeField] private int KillMonsterExp;//击杀怪物所得经验

    [Header("经验UI部分")]
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text ExpNumberText;
    [SerializeField] private TMP_Text LevelUpRequireExpNumberText;
    public int EvolutionCount = 0;


    private int ComputeNextLevelRequireExp(int Level)//更新下一级所需经验
    {
        int RequireExp=Level*10;

        return RequireExp;
    }

   public void HandleMonsterDeath()
    {
        ExpNumber+=KillMonsterExp;


        ExpNumberText.text = ExpNumber.ToString();

    }


    public int GetExpNumber() {


        return ExpNumber;
    }

    public void HandleExpNumber(int num)
    {
        ExpNumber += num;
    }



    public void LevelUIUpdate()
    {
        LevelText.text = Level.ToString();
        ExpNumberText.text = ExpNumber.ToString();
        LevelUpRequireExpNumberText.text = LevelUpRequireExpNumber.ToString();
       
    }


    private void Awake()
    {
        LevelUIUpdate();
    }

    private void Update()
    {

        if (ExpNumber> LevelUpRequireExpNumber)
        {
            Level++;
            EvolutionCount++;
            LevelUpRequireExpNumber= ComputeNextLevelRequireExp(Level);
            LevelUIUpdate();
        }
    }

}
