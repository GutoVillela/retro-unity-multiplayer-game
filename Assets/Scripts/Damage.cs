public class Damage
{
    public Damage(float damageToApply, bool isCritical)
    {
        DamageToApply = damageToApply;
        IsCritical = isCritical;
    }

    public float DamageToApply { get; private set; }
    public bool IsCritical { get; private set; }
}