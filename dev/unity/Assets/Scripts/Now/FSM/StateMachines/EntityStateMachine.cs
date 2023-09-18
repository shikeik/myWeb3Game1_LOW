using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Windows;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityStateMachine : StateMachine
    {
        public IEntityBehaviour input;
        public IEntityProperties props;
        public Animator anim;

        protected string IdleAnimName = "Idle";
        protected string MoveAnimName = "Move";
        protected string JumpAnimName = "Jump";
        protected string AttackAnimName = "Attack";
        protected string HurtAnimName = "Hurt";
        protected string DeathAnimName = "Death";

        public virtual void Awake()
        {
            input = GetComponent<IEntityBehaviour>();
            props = GetComponent<IEntityProperties>();
            anim = GetComponent<Animator>();
        }

        public override IState InitializeStates()
        {
            states.Add(typeof(EntityIdleState), new EntityIdleState(this, 0, IdleAnimName));
            states.Add(typeof(EntityMoveState), new EntityMoveState(this, 1, MoveAnimName));
            states.Add(typeof(EntityJumpState), new EntityJumpState(this, 2, JumpAnimName));
            states.Add(typeof(EntityAttackState), new EntityAttackState(this, 3, AttackAnimName));
            states.Add(typeof(EntityHurtState), new EntityHurtState(this, 4, HurtAnimName));
            states.Add(typeof(EntityDeathState), new EntityDeathState(this, 5, DeathAnimName));

            props.OnHurtEvent += GetState<EntityHurtState>().OnHurt;

            return GetState<EntityIdleState>();

        }

        public override void UpdateStateData()
        {

            GetState<EntityMoveState>().Direction = input.MoveX;

            GetState<EntityJumpState>().StateSwitch = input.JumpKeyDown;
            GetState<EntityJumpState>().IsGround = props.IsGround;
            GetState<EntityJumpState>().Direction = input.MoveX;

            GetState<EntityAttackState>().StateSwitch = input.AttackKeyDown;

            GetState<EntityDeathState>().StateSwitch = props.Health == 0;
        }

        public override void Update()
        {

            UpdateStateData();
            base.Update();
        }

        public void ClearFrame()
        {
            GameObject.Destroy(gameObject);
        }

        public bool IsAnimEnd()
        {
            // 获取当前动画状态
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            // 判断当前动画是否已经播放完毕
            if (stateInfo.normalizedTime >= 1.0f && !stateInfo.loop) return true;
            return false;
        }
    }
}
