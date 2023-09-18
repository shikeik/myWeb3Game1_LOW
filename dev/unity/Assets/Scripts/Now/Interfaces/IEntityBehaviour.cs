using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public interface IEntityBehaviour
    {
        int MoveX { get; }
        bool RunKeyDown { get; }
        bool JumpKeyDown { get; }
        bool AttackKeyDown { get; }
    }
}
