using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    [RequireComponent(typeof(IMovable))]
    public abstract class StateMachine : MonoBehaviour
    {
        public Dictionary<Type, IState> states = new Dictionary<Type, IState>();
        protected IState defaultState;
        protected IState currentState;

        public IState CurrentState { get=> currentState; }

        public abstract IState InitializeStates();

        protected void Start()
        {
            //初始化状态
            currentState = defaultState = InitializeStates();
        }

        public T GetState<T>()
        {
            if (states.ContainsKey(typeof(T)))
                return (T)(object)states[typeof(T)];

            return default;
        }
        
        public IState GetState(Type t){
            if (states.ContainsKey(t))
                return states[t];
            
            return default;
		}
        

        public virtual bool EnterState(Type t)
        {
            return EnterState(t, false);
        }

        public bool EnterState(Type t, bool skip)
        {
            if (!states.ContainsKey(t) ||
                states[t].Equals(currentState) ||
                (!skip && currentState.Priority > states[t].Priority)) return false;

            if (states[t].Enter())
            {
                currentState = states[t];
                return true;
            }

            return false;
        }


        public abstract void UpdateStateData();

        public virtual void Update()
        {

            if (currentState.Exit()) EnterState(defaultState.GetType(), true);

            foreach (Type t in states.Keys) if (EnterState(t)) break;

            currentState.Running();
        }

        private void FixedUpdate()
        {
            
            //currentState.Running();
        }
    }
}
