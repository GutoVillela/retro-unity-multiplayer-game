using System.Collections.Generic;

namespace Assets
{
    public class Constants
    {
        public const string PlayerNameProperty = "Name";
        public const string PlayerAvatarProperty = "PlayerAvatar";
        public const string PlayerIsTurnProperty = "IsTurn";

        public static List<AttackOption> PositiveAttacks = new List<AttackOption>()
        {
            new AttackOption(title: "Ataque positivo 1", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 2", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 3", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 4", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 5", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 6", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 7", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 8", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 9", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque positivo 10", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
        };

        public static List<AttackOption> NegativeAttacks = new List<AttackOption>()
        {
            new AttackOption(title: "Ataque negativo 1", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 2", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 3", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 4", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 5", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 6", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 7", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 8", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 9", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ataque negativo 10", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
        };
    }
}
