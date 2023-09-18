using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    [RequireComponent(typeof(EntityStateMachine))]
    public class InputController : MonoBehaviour, IEntityBehaviour
    {
        private PlayerInputControl input;
        private dynamic inputPlay;

        public enum InputPlayer { Letter, Number }
        public InputPlayer InputMode = InputPlayer.Letter;

        private int moveX;
        public int MoveX
        {
            get
            {
                //Debug.Log("get-MoveX: "+moveX);
                return moveX;
            }
            private set
            {
                moveX = value;
            }
        }
        public bool RunKeyDown { get; private set; }
        public bool JumpKeyDown { get; private set; }
        public bool AttackKeyDown { get; private set; }

        private void Awake()
        {
            input = new PlayerInputControl();
            inputPlay = InputMode.Equals(InputPlayer.Letter)?input.GamePlay:input.GamePlay2;
            input.GamePlay.Run.started += RunKey;
            input.GamePlay2.Run.started += RunKey;

        }

        private void RunKey(InputAction.CallbackContext context)
        {
            RunKeyDown = !RunKeyDown;
        }

        private void Update()
        {
            UpdateKey();
        }

        private void UpdateKey()
        {
            MoveX = (int)inputPlay.MoveX.ReadValue<Vector2>().normalized.x;
            //Debug.Log("InputController-Movex: "+MoveX);
            JumpKeyDown = inputPlay.Jump.IsPressed();
            AttackKeyDown = inputPlay.Attack.IsPressed();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }
    }
}