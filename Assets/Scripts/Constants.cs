using System.Collections.Generic;

namespace Assets
{
    public class Constants
    {
        public const string PlayerNameProperty = "Name";
        public const string PlayerAvatarProperty = "PlayerAvatar";
        public const string PlayerIsReadyProperty = "IsReady";
        public const string PlayerIsTurnProperty = "IsTurn";

        public static List<AttackOption> PositiveAttacks = new List<AttackOption>()
        {
            new AttackOption(title: "Ele odeia front-end", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele ama Java", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele jogar ping-pong no presencial é uma obrigação", minDamage: 0F, maxDamage: 15f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele o churrasco é de lei pelo menos 2x no ano", minDamage: 0F, maxDamage: 15f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele filho de verdade só de gente mesmo", minDamage: 10F, maxDamage: 20f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele acha que ser liberal é uma escolha ruim", minDamage: 10F, maxDamage: 20f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele disse que nunca votaria no Kim Kataguiri", minDamage: 15F, maxDamage: 20f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele ama cerveja", minDamage: 15F, maxDamage: 25f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele aprecia um bom Campari", minDamage: 15F, maxDamage: 30f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele um barzinho, umas brejas e a equipe no Happy Hour é indispensável", minDamage: 20F, maxDamage: 30f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele comemoria anualmente o arrasta pra cima do Olavo de C.", minDamage: 20F, maxDamage: 35f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele odeia os liberais", minDamage: 20F, maxDamage: 40f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele o Carlão é uma entidade divina", minDamage: 20F, maxDamage: 40F, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Em todo KickOff ele está lá mandando coraçãozinho pra você", minDamage: 20F, maxDamage: 45f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele nunca fala mal do Haddad, a segunda alma mais honesta do Brasil", minDamage: 20F, maxDamage: 45f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele a família toda do Bozo ia de xilindró", minDamage: 20F, maxDamage: 50f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele ama o Lula, a alma mais honesta do Brasil", minDamage: 20F, maxDamage: 50f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele odeia o Bolsonaro", minDamage: 20F, maxDamage: 50f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele odeia o Bolsonaro e ama o Lula", minDamage: 20F, maxDamage: 50f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele adora a Heleninha", minDamage: 50F, maxDamage: 100f, criticalMultiplier:2, isNegativeAttack: false, criticalChancesInPercentage: .25f),
        };

        public static List<AttackOption> NegativeAttacks = new List<AttackOption>()
        {
            new AttackOption(title: "Ele gosta de front-end", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele ama CSS", minDamage: 0F, maxDamage: 10f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .35f),
            new AttackOption(title: "Ele odeia Java", minDamage: 0F, maxDamage: 15f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele Scrum Master é indispensável", minDamage: 5F, maxDamage: 15f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .35f),
            new AttackOption(title: "Ele disse que você tem sotaque mais carioca que os cariocas", minDamage: 10F, maxDamage: 15f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele odeia os happy hours", minDamage: 10F, maxDamage: 15f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele acha que todo comunista vai pro inferno", minDamage: 10F, maxDamage: 20f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele acha que as confraternizações são perca de tempo", minDamage: 15F, maxDamage: 25f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Pra ele filho de verdade são só os pets", minDamage: 20F, maxDamage: 35f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele já disse que não gosta do Carlão", minDamage: 20F, maxDamage: 35f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: 1.0f),
            new AttackOption(title: "Ele chorou de tristeza quando o Olavo de C. foi de arrasta pra cima", minDamage: 20F, maxDamage: 40f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele acha que o comunismo é uma praga", minDamage: 20F, maxDamage: 40f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele acha que ser liberal é o melhor caminho", minDamage: 20F, maxDamage: 40f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele vota no MBL", minDamage: 30F, maxDamage: 40f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele é isentão", minDamage: 30F, maxDamage: 45f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele odeia o Lula, a alma mais honesta do Brasil", minDamage: 30F, maxDamage: 45f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele é crente liberal", minDamage: 30F, maxDamage: 50f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele votou no Bolsonaro", minDamage: 40F, maxDamage: 50f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele é crente e votou no Bolsonaro", minDamage: 40F, maxDamage: 55f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
            new AttackOption(title: "Ele não gosta de Heleninha", minDamage: 50F, maxDamage: 100f, criticalMultiplier:2, isNegativeAttack: true, criticalChancesInPercentage: .25f),
        };
    }
}
