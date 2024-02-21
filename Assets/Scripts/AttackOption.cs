using UnityEngine;

namespace Assets
{
    public class AttackOption
    {
        public AttackOption(string title, float minDamage, float maxDamage, float criticalMultiplier,  bool isNegativeAttack, float criticalChancesInPercentage)
        {
            Title = title;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            CriticalMultiplier = criticalMultiplier;
            IsNegativeAttack = isNegativeAttack;
            CriticalChancesInPercentage = criticalChancesInPercentage;
        }

        public string Title { get; private set; }
        public float MinDamage { get; private set; } = 0f;
        public float MaxDamage { get; private set; }
        public float CriticalMultiplier { get; private set; }
        public bool IsNegativeAttack { get; private set; }
        public float CriticalChancesInPercentage { get; private set; }

        public Damage Damage(Avatar avatar)
        {
            bool isCritical = Random.value <= CriticalChancesInPercentage;
            float damage = Random.Range(MinDamage, MaxDamage);
            float finalMultiplier = IsNegativeAttack ? -1f : 1f;

            if(isCritical)
            {
                damage = damage * CriticalMultiplier * finalMultiplier;
            } 
            else
            {
                damage = damage * finalMultiplier;
            }

            bool isSkillActivated = Random.value <= avatar.skillActivationChanceInPercentage;

            return new Damage(damage, isCritical, CriticalMultiplier, isSkillActivated, avatar.skillActivationDescription);
        }
    }
}
