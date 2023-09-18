using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityAnimBehaviours : MonoBehaviour
    {
        public PlayerStateMachine machine;


        public void OnEnable()
        {
            machine = GetComponent<PlayerStateMachine>();
        }

        public void OnLandAnimEnd()
        {
            machine.GetState<PlayerLandState>().StateSwitch = false;
            //Debug.Log("PlayerLand End");
        }
    }
}
