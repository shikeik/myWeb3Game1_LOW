using System;
using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityJumpState : State<EntityStateMachine>
    {
        //定义
        //为了遏制斜坡进入fall状态, 暂时效果不大,应该改为距离地面距离才可以.
        private float fallVelocity = -2f;
        private float fallDistance = 0.5f;

        //实时
        public bool IsGround { get; set; }

        //跳跃中移动的可选
        public int Direction { get; set; }

        protected EntityJumpState() { }
        public EntityJumpState(EntityStateMachine machine, int priority, string animName) : base(machine, priority, animName) { }


        public override bool Enter()
        {

            //起跳
            if (StateSwitch && IsGround)
            {
                machine.props.Jump();
                return true;
            }

            //跌落
            var toGroundDistance = ToGroundDistance();
            if (machine.props.Velocity.y < fallVelocity && !IsGround && toGroundDistance > fallDistance)
                return true;

            return false;
        }

        public override bool Exit()
        {
            var toGroundDistance = ToGroundDistance();
            if (machine.props.Velocity.y < 0 && toGroundDistance < fallDistance)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 状态运行语句
        /// </summary>
        public override void Running()
        {

            PlayJump();

            if(!machine.input.RunKeyDown)
                machine.GetState<EntityMoveState>().Move();
            else machine.GetState<EntityRunState>().Move();
        }


        protected virtual void PlayJump()
        {
            machine.anim.Play(AnimName);
        }

        /// <summary>
        /// 距离地面一定距离才算跌落, 防止斜坡跌落动画
        /// </summary>
        /// <returns></returns>
        private float ToGroundDistance()
        {
            RaycastHit2D hit = Physics2D.Raycast(machine.transform.position, Vector2.down);
            if (hit)
            {
                return hit.distance;
            }
            return -1;

        }
    }
}