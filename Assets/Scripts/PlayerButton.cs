using Assets;
using Assets.Events;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameManager _gameManager;
    public Player Player { get; private set; }

    private string _name;
    public string Name 
    {
        get => _name;
        private set 
        { 
            _name = value;
            if (_playerNameText != null)
            {
                _playerNameText.text = _name;
            }
        } 
    }

    private bool _isPlayerTurn;
    //public bool IsPlayerTurn 
    //{
    //    get => _isPlayerTurn;
    //    private set
    //    {
    //        _isPlayerTurn = value;
    //        Player.CustomProperties[Constants.PlayerIsTurnProperty] = _isPlayerTurn;
    //        PhotonNetwork.SetPlayerCustomProperties(Player.CustomProperties);
    //    }
    //}
    public int PlayerAvatar { get; private set; }
    public bool IsLocalPlayer { get; set; }
    public bool IsPlayerLost { get; set; } = false;

    private float _healthPoints = 100f;
    public float HealthPoints
    {
        get { return _healthPoints; }
        private set
        {
            Debug.Log("Nova vida: " + value);
            _healthBar.value = value;
            _healthPoints = value;
            if(_healthPoints <= 0)
            {
                NotifyPlayerLost();
            }
        }
    }

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _healthBar.maxValue = 100f;
        _healthBar.minValue = 0;
        _healthBar.SetValueWithoutNotify(100f);
    }

    private void NotifyPlayerLost()
    {
        IsPlayerLost = true;
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerLost, Player.UserId, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void SetPlayerInfo(Player player)
    {
        Name = player.NickName;
        Player = player;
        //IsPlayerTurn = false;
        if(player.CustomProperties.ContainsKey(Constants.PlayerAvatarProperty))
            PlayerAvatar = (int)player.CustomProperties[Constants.PlayerAvatarProperty];
        else
            PlayerAvatar = 0;
    }

    public void TakeDamage(AttackOption attackOption)
    {
        HealthPoints += attackOption.Damage();
        TakeDamageEvent takeDamageEvent = new TakeDamageEvent(Player.UserId, _healthPoints);
        object[] content = new object[] { takeDamageEvent.PlayerId, takeDamageEvent.NewHealth };
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerTakeDamage, content, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void SetHP(float hp)
    {
        HealthPoints = hp;
        Debug.Log($"Player {Name}'s new HP: {hp}");
    }

    public void OnPlayerButtonClicked()
    {
        if (!_gameManager.CanPlay) return;

        Debug.Log($"Clicked on player {Name}");
        _gameManager.SelectPlayer(Player.UserId);
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerSelectedOtherPlayer, Player.UserId, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    //public void SetPlayerTurn(bool isPlayerTurn)
    //{
    //    Debug.LogError($"Setting player turn ID: {Player.UserId} to = {isPlayerTurn}");
    //    IsPlayerTurn = isPlayerTurn;
    //}
}
