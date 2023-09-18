using System;
using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class PlayerLandState : State<PlayerStateMachine>
    {
        public PlayerLandState(PlayerStateMachine machine, int priority, string animName) : base(machine, priority, animName) { }

        public override bool Enter()
        {
            if (StateSwitch && machine.input.MoveX == 0)
            {
                //Debug.Log("Enter: PlayerLandState");
                machine.anim.Play(AnimName);
                return true;
            }

            return false;
        }

        public override bool Exit()
        {
            if(!StateSwitch)
            {
                //Debug.Log("Exit: PlayerLandState");
                return true;
            }
            return false;
        }

        public override void Running()
        {
        }
    }
}