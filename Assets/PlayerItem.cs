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

    ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private Sprite[] _avatars;

    private const string PlayerNameProperty = "Name";
    private const string PlayerAvatarProperty = "PlayerAvatar";

    public bool IsReady { get; private set; }

    private Player _player;
    public Player Player { get { return _player; } }

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
        _playerProperties[PlayerNameProperty] = _playerNameText.text;
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
        Debug.Log("Aplicando mudanças locais");
        if (_backgroundImage != null)
        {
            _backgroundImage.color = _highlightColor;
        }
        else
        {
            Debug.Log("NÃO ACHOU A IMAGEM");
        }

        _leftArrowButton.SetActive(true);
        _rightArrowButton.SetActive(true);
    }

    public void OnClickNextAvatarButton()
    {
        int currentAvatar = (int)_playerProperties[PlayerAvatarProperty];
        if (currentAvatar == _avatars.Length - 1)
            _playerProperties[PlayerAvatarProperty] = 0;
        else
            _playerProperties[PlayerAvatarProperty] = currentAvatar + 1;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
    }

    public void OnClickPreviousAvatarButton()
    {
        int currentAvatar = (int)_playerProperties[PlayerAvatarProperty];
        if (currentAvatar == 0)
            _playerProperties[PlayerAvatarProperty] = _avatars.Length - 1;
        else
            _playerProperties[PlayerAvatarProperty] = currentAvatar - 1;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
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
        if(player.CustomProperties.ContainsKey(PlayerAvatarProperty))
        {
            _playerAvatar.sprite = _avatars[(int)player.CustomProperties[PlayerAvatarProperty]];
            _playerProperties[PlayerAvatarProperty] = (int)player.CustomProperties[PlayerAvatarProperty];
        }
        else
        {
            _playerProperties[PlayerAvatarProperty] = 0;
        }
    }
}
