using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class PlayerStateMachine : EntityStateMachine
    {
        public new PlayerProperties props;

        private string[] JumpAnimNames;
        private string LandAnimName;

        private string invulnerabilityParamName;
        public float InvulnerabilityTicker { get; set; }

        public PlayerStateMachine()
        {
            IdleAnimName = "Idle";
            MoveAnimName = "Walk";
            JumpAnimNames = new string[]{ "Jump1", "Jump2", "Jump3", "Jump4" };
            LandAnimName = "Jump5";
            AttackAnimName = "Attack1";
            HurtAnimName = "Hurt";
            DeathAnimName = "Death";

            invulnerabilityParamName = "Blinking";
        }
        public override void Awake()
        {
            base.Awake();
            props = GetComponent<PlayerProperties>();
        }

        public override IState InitializeStates()
        {
            states.Add(typeof(EntityIdleState), new EntityIdleState(this, 0, IdleAnimName));
            states.Add(typeof(EntityMoveState), new EntityMoveState(this, 1, MoveAnimName));
            states.Add(typeof(EntityRunState), new EntityRunState(this, 2, "Run"));
            states.Add(typeof(PlayerJumpState), new PlayerJumpState(this, 3, JumpAnimNames));
            states.Add(typeof(PlayerLandState), new PlayerLandState(this, 4, LandAnimName));
            states.Add(typeof(EntityAttackState), new EntityAttackState(this, 5, AttackAnimName));
            states.Add(typeof(EntityHurtState), new EntityHurtState(this, 6, HurtAnimName));
            states.Add(typeof(EntityDeathState), new EntityDeathState(this, 7, DeathAnimName));

            props.OnHurtEvent += GetState<EntityHurtState>().OnHurt;

            return GetState<EntityIdleState>();

        }

        public override void UpdateStateData()
        {

            GetState<EntityRunState>().Direction = GetState<EntityMoveState>().Direction = input.MoveX;

            GetState<PlayerJumpState>().StateSwitch = input.JumpKeyDown;
            GetState<PlayerJumpState>().IsGround = props.IsGround;
            GetState<PlayerJumpState>().Direction = input.MoveX;

            if (input.MoveX != 0)
                GetState<PlayerLandState>().StateSwitch = false;

            GetState<EntityAttackState>().StateSwitch = input.AttackKeyDown;

            GetState<EntityDeathState>().StateSwitch = props.Health == 0;

            anim.SetBool(invulnerabilityParamName, props.Invulnerability);
            if (props.Invulnerability)
            {
                if(InvulnerabilityTicker >= props.InvulnerabilityDuration)
                {
                    props.Invulnerability = false;
                    InvulnerabilityTicker = 0;
                }
                InvulnerabilityTicker += Time.deltaTime;
            }
            //if (!props.IsGround && !input.JumpKeyDown && props.Rb.velocity.y >= -props.Rb.velocity.y / 5f)
            //{
            //    props.Rb.AddForce(new Vector2(0f, -(props.Rb.velocity.y * (150 / props.Rb.velocity.y))), ForceMode2D.Force);
            //}
            //新算法: 松开跳时以当前速度反比降为0, 更自然
            if (!props.IsGround && !input.JumpKeyDown && props.Velocity.y > 0)
            {
                props.Rb.velocity = new Vector2(props.Velocity.x, props.Velocity.y/(props.JumpForce/props.Velocity.y));
            }

        }

        //回调方法
        public void OnHurtFinish()
        {
            props.Invulnerability = true;
        }

    }
}
