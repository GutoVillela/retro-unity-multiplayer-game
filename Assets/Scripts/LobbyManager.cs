using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomNameInput;
    [SerializeField] private GameObject _joinOrCreateRoomPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private TMP_Text _roomName;

    [SerializeField] private RoomItem _roomItemPrefab;
    private List<RoomItem> _roomItems =  new();
    [SerializeField] private Transform _roomScrollViewContent;

    [SerializeField] private float _timeBetweenRoomUpdates = 5.0f;
    private float _nextUpdate;

    [SerializeField] private PlayerItem _playerItemPrefab;
    private List<PlayerItem> _playerItems = new();
    [SerializeField] private Transform _playerItemParent;

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _leaveRoomButton;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Sprite _readyButtonSprite;
    [SerializeField] private DialogBox _dialogBox;

    private bool _isReady = false;
    private const string PlayerIsReadyProperty = "IsReady";
    ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        _joinOrCreateRoomPanel.SetActive(true);
        _roomPanel.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    private void Update()
    {
        _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1);
        _startGameButton.enabled = IsAllPlayersReady();
    }

    public void OpenDialogBox(string title, string description)
    {
        _dialogBox.OpenDialogBox(title, description);
    }

    public void CloseDialogBox()
    {
        _dialogBox.CloseDialogBox();
    }

    public void JoinOnCreateRoom()
    {
        if (!string.IsNullOrEmpty(_roomNameInput.text))
        {
            PhotonNetwork.JoinOrCreateRoom(_roomNameInput.text, new RoomOptions() { MaxPlayers = 20, BroadcastPropsChangeToAll = true, PublishUserId = true }, TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom()
    {
        _joinOrCreateRoomPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.SetText(PhotonNetwork.CurrentRoom.Name);
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= _nextUpdate)
        {
            UpdateRoomList(roomList);
            _nextUpdate = Time.time + _timeBetweenRoomUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> roomList)
    {
        ClearCurrentRooms();
        foreach (RoomInfo roomInfo in roomList)
        {
            RoomItem newRoom = Instantiate(_roomItemPrefab, _roomScrollViewContent);
            newRoom.SetRoomName(roomInfo.Name);
        }
    }

    void ClearCurrentRooms()
    {
        foreach(var room in _roomItems)
        {
            Destroy(room.gameObject);
        }
        _roomItems.Clear();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        _joinOrCreateRoomPanel.SetActive(true);
        _roomPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        ClearPlayerList();

        if (PhotonNetwork.CurrentRoom == null)
            return;

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayer = Instantiate(_playerItemPrefab, _playerItemParent);
            newPlayer.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
                newPlayer.ApplyLocalChanges();
            else
                newPlayer.HideArrows();

            _playerItems.Add(newPlayer);
        }
    }

    private void ClearPlayerList()
    {
        foreach (var player in _playerItems)
        {
            Destroy(player.gameObject);
        }
        _playerItems.Clear();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void OnClickStartGameButton()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void OnReadyButtonClick()
    {
        _readyButton.enabled = false;
        _readyButton.GetComponent<Image>().sprite = _readyButtonSprite;

        PlayerItem playerItem = _playerItems.FirstOrDefault(x => x.Player == PhotonNetwork.LocalPlayer);
        playerItem.HideArrows();

        _isReady = true;
        _playerProperties[PlayerIsReadyProperty] = _isReady;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        var player = _playerItems.FirstOrDefault(x => x.Player == targetPlayer);
        if (player != null)
        {
            bool isReady = false;
            if (targetPlayer.CustomProperties.ContainsKey(PlayerIsReadyProperty))
            {
                isReady = (bool)targetPlayer.CustomProperties[PlayerIsReadyProperty];
            }
            else
            {
                _playerProperties[PlayerIsReadyProperty] = isReady;
            }
            if (isReady)
                player.Ready();
        }
    }

    private bool IsAllPlayersReady()
    {
        foreach (var player in _playerItems)
        {
            if (!player.IsReady) return false;
        }
        return true;
    }
}
