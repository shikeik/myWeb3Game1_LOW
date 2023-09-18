using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public interface IMovementEvent
    {
        void Move(int dir);
        void Jump();
    }
}