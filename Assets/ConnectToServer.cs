using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField _playerName;

    [SerializeField]
    private TMP_Text _buttonText;

    public void OnClickConnectToServer()
    {
        if (!string.IsNullOrEmpty(_playerName.text))
        {
            PhotonNetwork.NickName = _playerName.text;
            _buttonText.text = "Entrando na sala...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
