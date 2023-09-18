using System;
using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class EntityListener : MonoBehaviour
    {
        private InputController input;
        private EntityProperties props;
        private EntityStateMachine machine;

        [Header("Input")]
        public int moveDirection;
        public bool jumpKeyDown;

        [Header("Props")]
        public Vector2 velocity;
        public bool isGround;
        public float health;

        [Header("StateMachine")]
        public string currentState;
        public int runState_Direction;

        [Header("Animator")]
        public string currentAnim;

        // Use this for initialization
        void Start()
        {
            input = GetComponent<InputController>();
            props = GetComponent<EntityProperties>();
            machine = GetComponent<EntityStateMachine>();
        }

        // Update is called once per frame
        void Update()
        {
            moveDirection = input.MoveX;
            jumpKeyDown = input.JumpKeyDown;

            velocity = new Vector2(
                Mathf.Round(props.Velocity.x * 100) / 100,
                Mathf.Round(props.Velocity.y * 100) / 100);
            isGround = props.IsGround;
            health = props.Health;

            currentState = machine.CurrentState.GetType().Name;
            runState_Direction = machine.GetState<EntityMoveState>().Direction;

            currentAnim = machine.anim.GetCurrentAnimatorStateInfo(0).shortNameHash.ToString();
            Test();
        }

        private void Test()
        {
            //GetComponent<Rigidbody2D>().sharedMaterial.friction = meterialFriction;
        }
    }
}