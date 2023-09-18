using UnityEditor;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityIdleState : State<EntityStateMachine>
    {
        public EntityIdleState(EntityStateMachine machine, int priority, string animName) : base(machine, priority, animName) { }

        public override bool Enter()
        {
            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        public override void Running()
        {
            machine.anim.Play(AnimName);
        }
    }
}