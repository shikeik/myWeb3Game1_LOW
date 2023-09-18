using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityMoveState : State<EntityStateMachine>
    {
        private int direction;
        public int Direction
        {
            get
            {
                //Debug.Log("get-Direction: " + direction);
                return direction;
            }
            set
            {
                //Debug.Log("set-Direction: " + direction+", value: "+value);
                direction = value;
            }
        }
        public float Speed { get; protected set; }

        public EntityMoveState(EntityStateMachine machine, int priority, string animName) : base(machine, priority, animName)
        {
            Speed = machine.props.Speed;
        }

        public override bool Enter()
        {
            if (Direction != 0) return true;
            return false;
        }

        public override bool Exit()
        {
            if (Direction == 0) return true;
            return false;
        }

        public override void Running()
        {
            machine.anim.Play(AnimName);

            //Debug.Log("TestMove(): " + "EntityMoveState.Direction: " + Direction);//单元测试
            Move();
        }

        public void Move()
        {
            machine.props.Move(Direction);
        }
    }
}