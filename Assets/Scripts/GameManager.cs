using Assets;
using Assets.Events;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum HitType
{
    AddHp,
    SubtractHp
}

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerButton _playerButtonPrefab;
    private List<PlayerButton> _playerButtons = new();
    [SerializeField] private Transform _playerButtonParent;

    [SerializeField] private TMP_Text _currentPlayerTurnName;
    [SerializeField] private TMP_Text _selectedPlayerNameText;
    [SerializeField] private GameObject _hitTypeSelection;

    [SerializeField] private TMP_Text _firstOptionText;
    [SerializeField] private TMP_Text _secondOptionText;
    [SerializeField] private TMP_Text _thirdOptionText;

    [SerializeField] private GameObject _playerTurnStartPage;
    [SerializeField] private GameObject _optionSelectionPage;
    [SerializeField] private GameObject _gameOverPage;
    [SerializeField] private TMP_Text _gameOverText;

    [SerializeField] private AudioClip _playerTurnStartSound;
    [SerializeField] private AudioClip _loseHpSound;
    [SerializeField] private AudioClip _addHpSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CriticalTextEffect _criticalTextEffect;
    [SerializeField] private SkillEffect _skillEffect;
    [SerializeField] private HitText _hitText;

    [SerializeField] private Button _addHpButton;
    [SerializeField] private Button _subtractHpButton;

    private AttackOption _attackOption1;
    private AttackOption _attackOption2;
    private AttackOption _attackOption3;

    public bool IsLocalPlayerTurn { get; private set; }
    public bool IsGameOver { get; private set; } = false;
    public bool CanPlay { get => IsLocalPlayerTurn && !IsGameOver; }

    public PlayerButton CurrentSelectedPlayer { get; private set; }
    private string _playerTurnId;

    public override void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    public override void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void Awake()
    {
        _hitTypeSelection.SetActive(false);
        _playerTurnStartPage.SetActive(false);
        _optionSelectionPage.SetActive(false);
        _gameOverPage.SetActive(false);
        UpdatePlayerList();
        SelectFirstPlayer();
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        switch (obj.Code)
        {
            case NetworkEvents.PlayerSelectedOtherPlayer:
                string userId = (string)obj.CustomData;
                SelectPlayer(userId);
                break;
            case NetworkEvents.PlayerSelectedHitType:
                HitType hitType = (HitType)obj.CustomData;
                SetSelectedHitTypeInterface(hitType);
                break;
            case NetworkEvents.QuestionSorted:
                string indexesData = (string)obj.CustomData;
                List<int> indexesSorted = indexesData.Split(',').Select(int.Parse).ToList();
                HitType hitTypeSorted = indexesSorted.Last() == 0 ? HitType.AddHp : HitType.SubtractHp;
                indexesSorted.RemoveAt(indexesSorted.Count - 1);
                SetSortedQuestions(indexesSorted, hitTypeSorted);
                break;
            case NetworkEvents.PlayerChooseOption:
                if (PhotonNetwork.IsMasterClient)
                    SelectNextPlayerTurn();
                break;
            case NetworkEvents.PlayerTurnChanged:
                string turnUserId = (string)obj.CustomData;
                SetCurrentPlayerTurn(turnUserId);
                break;
            case NetworkEvents.PlayerTakeDamage:
                object[] damageData = (object[])obj.CustomData;
                TakeDamageEvent takeDamageEvent = new TakeDamageEvent((string)damageData[0], (float)damageData[1]);
                var playerThatTookDamage = _playerButtons.First(x => x.Player.UserId == takeDamageEvent.PlayerId);
                playerThatTookDamage.SetHP(takeDamageEvent.NewHealth);
                if(playerThatTookDamage.IsPlayerLost)
                {
                    _playerButtons.Remove(playerThatTookDamage);
                    playerThatTookDamage.RemovePlayer();
                }
                break;
            case NetworkEvents.PlayerActivateSkill:
                string skillDescription = (string)obj.CustomData;
                _skillEffect.ShowSkill(skillDescription);
                break;
            case NetworkEvents.PlayerLost:
                string lostPlayerId = (string)obj.CustomData;
                HandlePlayerLostNotification(lostPlayerId);
                break;
            default:
                Debug.LogError($"Unknown event type: {obj.Code}");
                break;
        }

        //float damage = (float)obj.CustomData;
    }

    private void HandlePlayerLostNotification(string playerId)
    {
        var player = _playerButtons.First(x => x.Player.UserId == playerId);
        player.IsPlayerLost = true;
        Debug.Log($"Player {player.Name} lost");

        if(_playerButtons.Where(x => !x.IsPlayerLost).Count() == 1)
        {
            HandleGameOver();
        }
    }

    private void HandleGameOver()
    {
        IsGameOver = true;
        var winner = _playerButtons.Single(x => !x.IsPlayerLost);
        _gameOverText.SetText($"O ganhador puxa-saco é: {winner.Name}");
        _gameOverPage.SetActive(true);
        _hitTypeSelection.SetActive(false);
        _playerTurnStartPage.SetActive(false);
        _optionSelectionPage.SetActive(false);
        _audioSource.PlayOneShot(_gameOverSound);
    }

    public void SelectPlayer(string playerId)
    {
        PlayerButton player =  _playerButtons.First(x => x.Player.UserId == playerId);
        CurrentSelectedPlayer = player;
        _selectedPlayerNameText.SetText($"Alvo: {player.Name}");
        _hitTypeSelection.SetActive(true);
        _playerTurnStartPage.SetActive(false);
        _optionSelectionPage.SetActive(false);
    }

    private void SelectFirstPlayer()
    {
        if (!PhotonNetwork.IsMasterClient) return; // Only master clients can sort players

        string firstUserId = _playerButtons[0].Player.UserId;
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerTurnChanged, firstUserId, RaiseEventOptions.Default, SendOptions.SendReliable);
        SetCurrentPlayerTurn(firstUserId);
    }

    void UpdatePlayerList()
    {
        ClearPlayerList();

        if (PhotonNetwork.CurrentRoom == null)
            return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players.OrderBy(x => x.Value.NickName))
        {
            PlayerButton newPlayer = Instantiate(_playerButtonPrefab, _playerButtonParent);
            newPlayer.SetPlayerInfo(player.Value);
            if (player.Value.IsLocal)
                newPlayer.IsLocalPlayer = true;
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

    private void SetLocalPlayerTurn()
    {
        //_addHpButton.SetEnabled(true);
        //_subtractHpButton.SetEnabled(true);
        _currentPlayerTurnName.SetText($"Agora é a sua vez");
        Debug.Log("It IS current player's turn");
        IsLocalPlayerTurn = true;
        _playerTurnStartPage.SetActive(true);
        _hitTypeSelection.SetActive(false);
        _optionSelectionPage.SetActive(false);
        //_playerButtons.Single(x => x.Player.UserId == PhotonNetwork.LocalPlayer.UserId).SetPlayerTurn(true);
        //SetOtherPlayersTurnToFalse();
    }

    private void SetSelectedHitTypeInterface(HitType hitType)
    {
        _optionSelectionPage.SetActive(true);
        _playerTurnStartPage.SetActive(false);
        _hitTypeSelection.SetActive(false);

        if(PhotonNetwork.IsMasterClient)
            SelectNextQuestions(hitType);
    }

    private void SelectNextQuestions(HitType hitType)
    {
        List<int> indexes = new List<int>();

        if (hitType == HitType.AddHp)
        {
            indexes = GenerateRandomIndexes(Constants.PositiveAttacks.Count);
        }
        else
        {
            indexes = GenerateRandomIndexes(Constants.NegativeAttacks.Count);
        }

        
        SetSortedQuestions(indexes, hitType);
        indexes.Add((int)hitType);
        string indexesString = string.Join(",", indexes);
        PhotonNetwork.RaiseEvent(NetworkEvents.QuestionSorted, indexesString, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    private void SetSortedQuestions(List<int> indexes, HitType hitType)
    {
        List<AttackOption> attackOptions = new List<AttackOption>();
        if (hitType == HitType.AddHp)
        {
            foreach (int index in indexes)
            {
                attackOptions.Add(Constants.PositiveAttacks[index]);
            }
        }
        else
        {
            foreach (int index in indexes)
            {
                attackOptions.Add(Constants.NegativeAttacks[index]);
            }
        }
        _attackOption1 = attackOptions[0];
        _attackOption2 = attackOptions[1];
        _attackOption3 = attackOptions[2];
        _firstOptionText.SetText(_attackOption1.Title);
        _secondOptionText.SetText(_attackOption2.Title);
        _thirdOptionText.SetText(_attackOption3.Title);
    }

    private List<int> GenerateRandomIndexes(int count)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < count; i++)
        {
            indexes.Add(i);
        }

        // Embaralhar os índices usando o algoritmo de Fisher-Yates
        for (int i = indexes.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = indexes[i];
            indexes[i] = indexes[randomIndex];
            indexes[randomIndex] = temp;
        }

        return indexes.GetRange(0, Mathf.Min(3, indexes.Count));
    }

    public void OnAddHpHitTypeClicked()
    {
        if (!CanPlay) return;

        Debug.Log("Clicked on HELP");
        SetSelectedHitTypeInterface(HitType.AddHp);
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerSelectedHitType, HitType.AddHp, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void OnSubtractHpHitTypeClicked()
    {
        if (!CanPlay) return;

        Debug.Log("Clicked on NOT HELP");
        SetSelectedHitTypeInterface(HitType.SubtractHp);
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerSelectedHitType, HitType.SubtractHp, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    private void SetCurrentPlayerTurn(string playerId)
    {
        _playerTurnStartPage.SetActive(true);
        _hitTypeSelection.SetActive(false);
        _optionSelectionPage.SetActive(false);
        _playerTurnId = playerId;
        Debug.Log($"Player turn {playerId} ---- Local player {PhotonNetwork.LocalPlayer.UserId}");
        if (playerId == PhotonNetwork.LocalPlayer.UserId)
            SetLocalPlayerTurn();
        else
        {
            PlayerButton currentTurnPlayer = _playerButtons.Single(x => x.Player.UserId == playerId);
            Debug.Log($"Current player turn {currentTurnPlayer.Player.UserId}");
            IsLocalPlayerTurn = false;
            _currentPlayerTurnName.SetText($"Agora é a vez do {currentTurnPlayer.Name}");
        }
    }


    public void OnFirstOptionClicked()
    {
        if (!CanPlay) return;

        ApplyDamage(_attackOption1);
    }

    public void OnSecondOptionClicked()
    {
        if (!CanPlay) return;

        ApplyDamage(_attackOption2);

    }

    public void OnThirdOptionClicked()
    {
        if (!CanPlay) return;

        ApplyDamage(_attackOption3);
    }

    private void ApplyDamage(AttackOption selectedOption)
    {
        Damage damageApplied = CurrentSelectedPlayer.TakeDamage(selectedOption);

        if(damageApplied.IsCritical && !damageApplied.IsSkillActivated)
        {
            _criticalTextEffect.ShowCriticalText();
        }

        if(CurrentSelectedPlayer.IsPlayerLost){
            _playerButtons.Remove(CurrentSelectedPlayer);
            CurrentSelectedPlayer.RemovePlayer();
        }

        _hitText.ShowHitText(damageApplied.DamageText(), CurrentSelectedPlayer.transform.position);
        IsLocalPlayerTurn = false; // After applying damage this player can't play anymore
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerChooseOption, 0, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
        
        if(damageApplied.DamageToApply > 0)
            _audioSource.PlayOneShot(_addHpSound);
        else
            _audioSource.PlayOneShot(_loseHpSound);
    }

    private void SelectNextPlayerTurn()
    {
        if (!PhotonNetwork.IsMasterClient) return; // Only master clients can sort players
        int currentPlayerIndex = _playerButtons.IndexOf(_playerButtons.First(x => x.Player.UserId == _playerTurnId));
        int nextPlayerIndex = currentPlayerIndex + 1;
        if(nextPlayerIndex == _playerButtons.Count)
            nextPlayerIndex = 0;

        //_playerButtons[currentPlayerIndex].SetPlayerTurn(false);
        //_playerButtons[nextPlayerIndex].SetPlayerTurn(true);
        string nextPlayerId = _playerButtons[nextPlayerIndex].Player.UserId;
        PhotonNetwork.RaiseEvent(NetworkEvents.PlayerTurnChanged, nextPlayerId, RaiseEventOptions.Default, SendOptions.SendReliable);
        SetCurrentPlayerTurn(nextPlayerId);
    }

    public void BackToMainScreen()
    {
        if(!IsGameOver) return;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }

}
