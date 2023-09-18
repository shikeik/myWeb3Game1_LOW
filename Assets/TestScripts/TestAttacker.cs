using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class TestAttacker : MonoBehaviour, ILivingBeing
    {
        [SerializeField] private float damage = 5;
        public float Damage { get => damage; }

        public float Health { get; }

        public event Action OnHurtEvent;
        public event Action OnDeathEvent;

        public void OnHurt(float damage)
        {
        }

        public void TakeDamage(ILivingBeing target)
        {
            target.OnHurt(Damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ILivingBeing target = collision.GetComponent<ILivingBeing>();

            if (target != null)
            {
                Debug.Log("TargetHealth: " + target.Health);

                TakeDamage(target);

                Debug.Log("TakeDamage: " + ", " + collision.name + ": " + Damage + "tohealth: " + target.Health);
            }
        }
    }
}