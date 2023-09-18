using Assets.Scripts.Now;
using System;
using System.Collections;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(IsGroundCheck))]
    public class PlayerProperties : EntityProperties
    {
        [SerializeField] private float invulnerabilityDuration = 2;
        public bool Invulnerability { get; set; }
        public float InvulnerabilityDuration => invulnerabilityDuration;

        public PlayerProperties()
        {
            speed = 4.5f;
            jumpForce = 16.5f;
            maxHealth = 20;
            damage = 5;
        }

        public override void OnHurt(float damage)
        {
            if(!Invulnerability)
                base.OnHurt(damage);
        }
    }
}