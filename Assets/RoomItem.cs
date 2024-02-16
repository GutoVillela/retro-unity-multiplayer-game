using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;

    private LobbyManager _lobbyManager;

    private void Start()
    {
        _lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string roomName)
    {
        _roomName.SetText(roomName);
    }

    public void OnClickRoomItem()
    {
        _lobbyManager.JoinRoom(_roomName.text);
    }
}
