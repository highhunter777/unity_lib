using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Property : MonoBehaviour
{
    [Header("�������ӳ�(%)")]
    public float attackBonus;
    [Header("������(%)")]
    public float CriticalRate;
    [Header("�����˺�(%)")]
    public float CriticalValue;
    [Header("������(%)(�ٷֱȼ���)")]
    public float Defense;
    [Header("�ƶ��ٶȼӳ�(%)")]
    public float MovementSpeedBonus;
    [Header("�����ٶȼӳ�(%)")]
    public float AttackSpeedBonus;
    [Header("������(%)")]
    public float EvasionRate;
    [Header("��Ѫ��(%)")]
    public float GetBloodRate;
    [Header("����ֵ")]
    public float RegenerationValue;
    [Header("������Χ")]
    public float AttackRange;
    [Header("�����ȡЧ��")]
    public float GetExpEfficiency;
    [Header("ʰȡ��Χ")]
    public float GetExpRange;


    public bool isLaunch = true;
    [SerializeField] private UnityEvent<float> MoveSpeedChanged;
    [SerializeField] private UnityEvent<float> AttackSpeedChanged;

    public void MovementSpeedBonusChange(float newChange)
    {
        MovementSpeedBonus += newChange;
        MoveSpeedChanged?.Invoke(MovementSpeedBonus);

    }

    public void AttackSpeedBonusChange(float newChange)
    {
        AttackSpeedBonus += newChange;
        AttackSpeedChanged?.Invoke(AttackSpeedBonus);

    }
}
