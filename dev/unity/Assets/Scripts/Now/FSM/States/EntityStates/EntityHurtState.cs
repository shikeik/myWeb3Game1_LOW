using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityHurtState : State<EntityStateMachine>
    {

        public EntityHurtState(EntityStateMachine machine, int priority, string animName) : base(machine, priority, animName) { }

        public override bool Enter()
        {
            if (StateSwitch)return true;
            return false;
        }

        public override bool Exit()
        {
            if (machine.IsAnimEnd())
            {
                StateSwitch = false;
                return true;
            }
            return false;
        }

        public override void Running()
        {
            machine.anim.Play(AnimName);
        }
        
        public virtual void OnHurt(){
            StateSwitch = true;
        }
    }
}