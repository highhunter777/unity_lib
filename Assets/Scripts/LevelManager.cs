using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("�������ݲ���")]
    [SerializeField] private int Level;//��ǰ�ȼ�
    [SerializeField] private int ExpNumber;//���������
    [SerializeField] private int LevelUpRequireExpNumber;//�������辭�������
    [SerializeField] private int KillMonsterExp;//��ɱ�������þ���

    [Header("����UI����")]
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text ExpNumberText;
    [SerializeField] private TMP_Text LevelUpRequireExpNumberText;
    public int EvolutionCount = 0;


    private int ComputeNextLevelRequireExp(int Level)//������һ�����辭��
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
