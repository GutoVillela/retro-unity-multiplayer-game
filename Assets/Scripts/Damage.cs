using System;
using Unity.Mathematics;

public class Damage
{
    public Damage(float damageToApply, bool isCritical, float criticalMultiplier, bool isSkillActivated, string skillDescription)
    {
        DamageToApply = damageToApply;
        IsCritical = isCritical;
        CriticalMultiplier = criticalMultiplier;
        IsSkillActivated = isSkillActivated;
        SkillDescription = skillDescription;
    }

    public float DamageToApply { get; private set; }
    public bool IsCritical { get; private set; }
    public float CriticalMultiplier { get; private set; }
    public bool IsSkillActivated { get; private set; }
    public string SkillDescription { get; private set; }

    public void OverrideDamage(float damageToApply)
    {
        DamageToApply = damageToApply;
    }

    public void CancelCritical()
    {
        if (!IsCritical) return;

        IsCritical = false;
        DamageToApply /= CriticalMultiplier;
    }

    public string DamageText()
    {
        int damage = (int)Math.Round(DamageToApply, 0);
        if (damage > 0)
            return $"+{DamageToApply}";
        else if (damage < 0)
            return $"{DamageToApply}";
        else
            return $"Miss";
    }
}