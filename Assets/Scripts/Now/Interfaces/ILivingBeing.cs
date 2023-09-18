using System;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    public interface ILivingBeing
    {
        event Action OnHurtEvent;
        event Action OnDeathEvent;

        float Health { get; }
        float Damage { get; }
        void TakeDamage(ILivingBeing target);

        void OnHurt(float damage);
    }
}