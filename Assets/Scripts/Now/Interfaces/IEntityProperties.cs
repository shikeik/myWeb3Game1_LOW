using System;
using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public interface IEntityProperties: IMovable, ILivingBeing
    {
        int DefaultFacing { get; }
    }
}