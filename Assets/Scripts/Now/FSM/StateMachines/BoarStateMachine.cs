using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Windows;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class BoarStateMachine : EntityStateMachine
    {
        public BoarStateMachine()
        {
            IdleAnimName = "Idle";
            MoveAnimName = "Walk";
            JumpAnimName = "Jump";
            AttackAnimName = "Attack";
            HurtAnimName = "Hurt";
            DeathAnimName = "Death";
        }
    }
}
