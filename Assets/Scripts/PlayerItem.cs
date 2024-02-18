using Assets;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviourPunCallbacks
{

    [SerializeField] private TMP_Text _playerNameText;

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private GameObject _leftArrowButton;
    [SerializeField] private GameObject _rightArrowButton;
    [SerializeField] private Button _descriptionButton;

    ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _avatarChangeSound;
    [SerializeField] private Avatar[] _avatarList;

    private LobbyManager _lobbyManager;

    public bool IsReady { get; private set; }

    private Player _player;
    public Player Player { get { return _player; } }

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
        _playerProperties[Constants.PlayerNameProperty] = _playerNameText.text;
        
        _lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void OpenDescription()
    {
        Avatar avatar = GetPlayerDescription((int)_playerProperties[Constants.PlayerAvatarProperty]);
        _lobbyManager.OpenDialogBox(avatar.name, avatar.description);
    }

    private Avatar GetPlayerDescription(int avatarIndex)
    {
        return _avatarList[avatarIndex];
    }

    public void Ready()
    {
        IsReady = true;
    }

    public void SetPlayerInfo(Player player)
    {
        _playerNameText.SetText(player.NickName);
        _player = player;
        UpdatePlayerInfo(player);
    }

    public void ApplyLocalChanges()
    {
        if (_backgroundImage != null)
        {
            _backgroundImage.color = _highlightColor;
        }

        _leftArrowButton.SetActive(true);
        _rightArrowButton.SetActive(true);
    }

    public void HideArrows()
    {
        _leftArrowButton.SetActive(false);
        _rightArrowButton.SetActive(false);
    }

    public void OnClickNextAvatarButton()
    {
        int currentAvatar = (int)_playerProperties[Constants.PlayerAvatarProperty];
        if (currentAvatar == _avatarList.Length - 1)
            _playerProperties[Constants.PlayerAvatarProperty] = 0;
        else
            _playerProperties[Constants.PlayerAvatarProperty] = currentAvatar + 1;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
        _audioSource.PlayOneShot(_avatarChangeSound);
    }

    public void OnClickPreviousAvatarButton()
    {
        int currentAvatar = (int)_playerProperties[Constants.PlayerAvatarProperty];
        if (currentAvatar == 0)
            _playerProperties[Constants.PlayerAvatarProperty] = _avatarList.Length - 1;
        else
            _playerProperties[Constants.PlayerAvatarProperty] = currentAvatar - 1;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
        _audioSource.PlayOneShot(_avatarChangeSound);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == _player)
        {
            UpdatePlayerInfo(targetPlayer);
        }
    }

    private void UpdatePlayerInfo(Player player)
    {
        if(player.CustomProperties.ContainsKey(Constants.PlayerAvatarProperty))
        {
            _playerAvatar.sprite = _avatarList[(int)player.CustomProperties[Constants.PlayerAvatarProperty]].sprite;
            _playerProperties[Constants.PlayerAvatarProperty] = (int)player.CustomProperties[Constants.PlayerAvatarProperty];
        }
        else
        {
            _playerProperties[Constants.PlayerAvatarProperty] = 0;
        }
    }
}
