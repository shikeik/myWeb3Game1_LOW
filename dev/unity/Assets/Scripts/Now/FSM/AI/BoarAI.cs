using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 一个基本的敌人AI: 
/// 1. 每间隔一段时间做出一个行动: 在待机和巡逻中选择
/// </summary>
public class BoarAI : MonoBehaviour, IEntityBehaviour
{
    private enum Behaviour { Idle, Move, Attack }
    private Behaviour currentBehaviour;

    [SerializeField] 
    private float tickerDuration = 1;
    private float ticker;

    public int MoveX { get; private set; }
    public bool RunKeyDown { get; private set; }
    public bool JumpKeyDown { get; private set; }
    public bool AttackKeyDown { get; private set; }

    private void Update()
    {
        TickerTask();

    }
    private void FixedUpdate()
    {

        AutoAttack();
    }
    /// <summary>
    /// 临时测试代码: 判断半径范围有玩家就攻击
    /// </summary>
    private void AutoAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1.8f);
        var player = cols.FirstOrDefault(collider => collider.GetComponent<PlayerProperties>());
        if (player != null)
        {
            currentBehaviour = Behaviour.Attack;
            UpdateBehaviour();

            //转头, 这里需要额外乘上自身默认朝向所以有耦合, 之后建议分离耦合: 设置引用方法
            var direction = (player.transform.position.x > transform.position.x ? 1 : -1) * transform.GetComponent<IEntityProperties>().DefaultFacing;
            transform.localScale = new Vector3(direction, 1, 1);
        }else if (currentBehaviour.Equals(Behaviour.Attack)) { SelectBehaviour(); }
        

    }

    private void UpdateBehaviour()
    {
        MoveX = 0;
        JumpKeyDown = false;
        AttackKeyDown = false;
        switch (currentBehaviour)
        {
            case Behaviour.Idle:
                break;

            case Behaviour.Move:
                MoveX = Random.Range(0, 2) < 0.5f ? -1 : 1;
                break;

            case Behaviour.Attack:
                AttackKeyDown = true;
                break;
        }
    }

    private void TickerTask()
    {
        if(ticker >= tickerDuration)
        {
            ticker = 0;
            SelectBehaviour();
        }
        ticker += Time.deltaTime;
    }

    //随机行动
    private void SelectBehaviour()
    {
        currentBehaviour = (Behaviour)Random.Range(0, Enum.GetValues(typeof(Behaviour)).Length);
        if (currentBehaviour.Equals(Behaviour.Attack)) return;//这里暂时不需要随机到攻击

        UpdateBehaviour();
    }
}
