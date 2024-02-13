using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    public Player Player { get; private set; }
    public string Name { get; private set; }
    public bool IsPlayerTurn {  get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Name = player.NickName;
        Player = player;
        IsPlayerTurn = false;
    }

    public void OnPlayerButtonClicked()
    {
        Debug.Log($"Clicked on player {Name}");
    }
}
