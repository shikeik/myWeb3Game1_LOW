using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityRunState : EntityMoveState
    {
        public EntityRunState(EntityStateMachine machine, int priority, string animName) : base(machine, priority, animName)
        {
            Speed = machine.props.Speed * 2f;
        }

        public override bool Enter()
        {
            if (Direction != 0 && machine.input.RunKeyDown) return true;
            return false;
        }

        public override bool Exit()
        {
            if (!machine.input.RunKeyDown || Direction==0) return true;
            return false;
        }
    }
}