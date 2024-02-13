using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerButton _playerButtonPrefab;
    private List<PlayerButton> _playerButtons = new();
    [SerializeField] private Transform _playerButtonParent;

    private void Awake()
    {
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        ClearPlayerList();

        if (PhotonNetwork.CurrentRoom == null)
            return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerButton newPlayer = Instantiate(_playerButtonPrefab, _playerButtonParent);
            newPlayer.SetPlayerInfo(player.Value);
            _playerButtons.Add(newPlayer);
        }
    }

    private void ClearPlayerList()
    {
        foreach (var player in _playerButtons)
        {
            Destroy(player.gameObject);
        }
        _playerButtons.Clear();
    }
}
