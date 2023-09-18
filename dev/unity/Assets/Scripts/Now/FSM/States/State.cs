using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn 
{
    public abstract class State<T> : IState where T: StateMachine
    {
        protected T machine;

        public int Priority { get; protected set; }
        public bool StateSwitch { get; set; }
        public string AnimName { get; set; }

        public State() { }
        public State(T machine) { this.machine = machine; }
        public State(int priority) { Priority = priority; }
        public State(T machine, int priority): this(machine) { Priority = priority; }
        public State(T machine, int priority, string animName): this(machine, priority) { AnimName = animName; }

        public abstract bool Enter();
        public abstract bool Exit();
        public abstract void Running();
    }
}