using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public interface IMovable: IMovementEvent
    {
        //引用属性
        Rigidbody2D Rb { get; }

        //设定属性
        float Speed { get; }
        float JumpForce { get; }

        //实时属性
        bool IsGround { get; }
        Vector2 Velocity { get; }

        
    }
}