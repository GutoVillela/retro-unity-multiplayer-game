
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Avatars
{
    public static List<Avatar> AvailableAvatars 
    { 
        get => new List<Avatar>()
        {
            new Avatar()
            {
                id = 0,
                name = "Feli Bombado",
                description = "O Feli Bombabo é um dos personagens mais fortes do mundo, e com certeza é o player mais forte do multiverso Alam. Seus poderes são do tamanho dos seus bíceps: imensos. Este personagem possui 20% de chance de ser confundido com o Florêncio e todo o dano de uma rodada ser transferido para ele.",
            },
            new Avatar()
            {
                id = 1,
                name = "Carlão Feliz",
                description = "O Carlão Feliz é uma entidade divina que nem se eu tivesse a inspiração de Shakespeare poderia descrever aqui, e se pudesse você, mero mortal, não entenderia. Ele possui 50% de chance de recuperar toda a vida quando for receber um golpe letal.",
            },
            new Avatar()
            {
                id = 2,
                name = "Florêncio Stand-Up",
                description = "Poucas pessoas possuem a graça do Florêncio Stand-Up. Nessa foto ele está rindo de uma piada que ele mesmo contou. É o ser mais engraçado do multiverso Alam. Por ser gente boa demais possui 60% de chance de anular um golpe crítico .",
            },
            new Avatar()
            {
                id = 3,
                name = "Nilo Sedutor",
                description = "As lendas dizem que quem encara essa foto por mais de 5 segundas se apaixona na hora. É tão multi tarefa que hora é TL e hora é PL (depende muito se Saturno está retrógrado). Possui 35% de chance de converter um dano em ganho da vida.",
            },
            new Avatar()
            {
                id = 4,
                name = "Guubal Pirata",
                description = "Muitas lendas contam como esse famoso pirata perdeu um dos olhos, mas a verdade até hoje nunca foi revelada. Possui 20% de chance de anular um golpe (vai estar no mecânico, ou não vai ver o golpe chegar).",
            },
            new Avatar()
            {
                id = 5,
                name = "Gus Mafioso",
                description = "Eu ia escrever uma longa descrição mas o Gus pediu pra resumir. Este personagem possui 30% de chance de pedir pra você resumir e portanto dividir o dano pela metade.",
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
}