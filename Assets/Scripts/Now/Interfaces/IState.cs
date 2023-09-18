using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public interface IState
    {
        int Priority { get; }
        bool StateSwitch{ get;set; }
        bool Enter();
        bool Exit();
        void Running();
    }
}