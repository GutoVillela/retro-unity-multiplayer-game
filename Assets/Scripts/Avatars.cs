
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Avatars
{
    public static int FeliBombadoId = 0;
    public static int CarlaoFelizId = 1;
    public static int FlorencioStandUpId = 2;
    public static int NiloSedutorId = 3;
    public static int GuubalPirataId = 4;
    public static int GusMafiosoId = 5;

    public static List<Avatar> AvailableAvatars 
    { 
        get => new List<Avatar>()
        {
            new Avatar()
            {
                id = FeliBombadoId,
                name = "Feli Bombado",
                description = "O Feli Bombabo é um dos personagens mais fortes do mundo, e com certeza é o player mais forte do multiverso Alam. Seus poderes são do tamanho dos seus bíceps: imensos. Este personagem possui 20% de chance de intimidar quem falar mal dele e anular todo o dano da rodada.",
                skillActivationChanceInPercentage = .2f,
                skillActivationDescription = "Você foi intimidado pelo Feli. O dano foi anulado!",
                skill = (Damage damage)  => {
                    damage.OverrideDamage(0f);
                    return damage;
                },
            },
            new Avatar()
            {
                id = CarlaoFelizId,
                name = "Carlão Feliz",
                description = "O Carlão Feliz é uma entidade divina que nem se eu tivesse a inspiração de Shakespeare poderia descrever aqui, e se pudesse você, mero mortal, não entenderia. Ele possui 5% de chance de recuperar toda a vida quando for receber um golpe.",
                skillActivationChanceInPercentage = .05f,
                skillActivationDescription = "O Carlão Feliz se salvou! Toda a vida foi recuperada!",
                skill = (Damage damage)  => {
                    damage.OverrideDamage(100f);
                    return damage;
                },
            },
            new Avatar()
            {
                id = FlorencioStandUpId,
                name = "Florêncio Stand-Up",
                description = "Poucas pessoas possuem a graça do Florêncio Stand-Up. Nessa foto ele está rindo de uma piada que ele mesmo contou. É o ser mais engraçado do multiverso Alam. Por ser gente boa demais possui 60% de chance de anular um golpe crítico .",
                skillActivationChanceInPercentage = .6f,
                skillActivationDescription = "O Florêncio Stand-Up é gente boa demais e anulou o golpe crítico!",
                skill = (Damage damage)  => {
                    damage.CancelCritical();
                    return damage;
                },
            },
            new Avatar()
            {
                id = NiloSedutorId,
                name = "Nilo Sedutor",
                description = "As lendas dizem que quem encara essa foto por mais de 5 segundas se apaixona na hora. É tão multi tarefa que hora é TL e hora é PL (depende muito se Saturno está retrógrado). Possui 35% de chance de converter um dano em ganho da vida.",
                skillActivationChanceInPercentage = .35f,
                skillActivationDescription = "O Nilo Sedutor te seduziu e converteu o dano em ganho de vida!",
                skill = (Damage damage)  => {
                    damage.OverrideDamage(Math.Abs(damage.DamageToApply));
                    return damage;
                },
            },
            new Avatar()
            {
                id = GuubalPirataId,
                name = "Guubal Pirata",
                description = "Muitas lendas contam como esse famoso pirata perdeu um dos olhos, mas a verdade até hoje nunca foi revelada. Possui 20% de chance de anular um golpe (vai estar no mecânico, ou não vai ver o golpe chegar).",
                skillActivationChanceInPercentage = .2f,
                skillActivationDescription = "O Guubal Pirata está no mecânico e anulou o golpe!",
                skill = (Damage damage)  => {
                    damage.OverrideDamage(0f);
                    return damage;
                },
            },
            new Avatar()
            {
                id = GusMafiosoId,
                name = "Gus Mafioso",
                description = "Eu ia escrever uma longa descrição mas o Gus pediu pra resumir. Este personagem possui 30% de chance de pedir pra você resumir e portanto dividir o dano pela metade.",
                skillActivationChanceInPercentage = .3f,
                skillActivationDescription = "O Gus Mafioso pediu pra você resumir. O dano foi dividido pela metade!",
                skill = (Damage damage)  => {
                    damage.OverrideDamage(damage.DamageToApply / 2);
                    return damage;
                },
            },
        }; 
    }

}


[System.Serializable]
public class Avatar
{
    public int id;
    public string name;

    [TextArea]
    public string description;
    public Sprite sprite;
    public float skillActivationChanceInPercentage;
    public string skillActivationDescription;
    public Func<Damage, Damage> skill;
}