using com.goldsprite.LegendOfWarriors3_StateMachineLearn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private float damage;
        public float Damage { get => damage; }

        public void Awake()
        {
            damage = GetComponentInParent<ILivingBeing>().Damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ILivingBeing target = collision.GetComponent<ILivingBeing>();
            if (target != null) target.OnHurt(Damage);
        }
    }
}