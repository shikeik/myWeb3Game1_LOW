using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityAttackState : State<EntityStateMachine>
    {

        public EntityAttackState(EntityStateMachine machine, int priority, string animName) : base(machine, priority, animName) { }

        public override bool Enter()
        {
            if (StateSwitch) return true;
            return false;
        }

        public override bool Exit()
        {
            if (!StateSwitch) return true;
            return false;
        }

        public override void Running()
        {
            machine.anim.Play(AnimName);

        }

        public void AttackKey()
        {
            StateSwitch = true;
        }
    }
}