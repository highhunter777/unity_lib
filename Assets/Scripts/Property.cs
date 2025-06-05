using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Property : MonoBehaviour
{
    [Header("攻击力加成(%)")]
    public float attackBonus;
    [Header("暴击率(%)")]
    public float CriticalRate;
    [Header("暴击伤害(%)")]
    public float CriticalValue;
    [Header("防御力(%)(百分比减伤)")]
    public float Defense;
    [Header("移动速度加成(%)")]
    public float MovementSpeedBonus;
    [Header("攻击速度加成(%)")]
    public float AttackSpeedBonus;
    [Header("闪避率(%)")]
    public float EvasionRate;
    [Header("吸血率(%)")]
    public float GetBloodRate;
    [Header("再生值")]
    public float RegenerationValue;
    [Header("攻击范围")]
    public float AttackRange;
    [Header("经验获取效率")]
    public float GetExpEfficiency;
    [Header("拾取范围")]
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
