using System;
using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class PlayerJumpState : EntityJumpState
    {
        public new string[] AnimName;
        private new PlayerStateMachine machine;


        public PlayerJumpState(PlayerStateMachine machine, int priority, string[] animName)
        {
            base.machine = this.machine = machine;
            base.Priority = priority;
            this.AnimName = animName;
        }

        public override bool Exit()
        {
            if (base.Exit())
            {
                machine.GetState<PlayerLandState>().StateSwitch = true;
                //Debug.Log("PlayerJumpState Enter: PlayerLandState");
                return true;
            }
            return false;

        }

        protected override void PlayJump()
        {
            float rate = machine.props.Velocity.y / machine.props.JumpForce;
            if(rate >= 0.8f){
                machine.anim.Play(AnimName[0]);
            }else if(rate >= 0.55f){
                machine.anim.Play(AnimName[1]);
            }else if(rate >= -0.4f){
                machine.anim.Play(AnimName[2]);
            }else{
                machine.anim.Play(AnimName[3]);
            }
        }
    }
}