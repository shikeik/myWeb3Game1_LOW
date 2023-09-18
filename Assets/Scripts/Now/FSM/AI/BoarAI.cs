using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// һ�������ĵ���AI: 
/// 1. ÿ���һ��ʱ������һ���ж�: �ڴ�����Ѳ����ѡ��
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
    /// ��ʱ���Դ���: �жϰ뾶��Χ����Ҿ͹���
    /// </summary>
    private void AutoAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1.8f);
        var player = cols.FirstOrDefault(collider => collider.GetComponent<PlayerProperties>());
        if (player != null)
        {
            currentBehaviour = Behaviour.Attack;
            UpdateBehaviour();

            //תͷ, ������Ҫ�����������Ĭ�ϳ������������, ֮����������: �������÷���
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

    //����ж�
    private void SelectBehaviour()
    {
        currentBehaviour = (Behaviour)Random.Range(0, Enum.GetValues(typeof(Behaviour)).Length);
        if (currentBehaviour.Equals(Behaviour.Attack)) return;//������ʱ����Ҫ���������

        UpdateBehaviour();
    }
}
