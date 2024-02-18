using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Transform _dialogBox;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private TMP_Text _dialogTitle;
    [SerializeField] private TMP_Text _dialogDescription;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _dialogBoxOpenSound;
    [SerializeField] private AudioClip _dialogBoxCloseSound;


    public void OpenDialogBox(string title, string description)
    {
        _dialogTitle.SetText(title);
        _dialogDescription.SetText(description);
        gameObject.SetActive(true);

        _background.alpha = 0;
        _background.LeanAlpha(1, 0.5f);

        _dialogBox.localPosition = new Vector2(0, -Screen.height);
        _dialogBox.LeanMoveLocalY(0, 0.5f).setEaseInExpo().setOnStart(OnStartOpenDialog).delay = 0.1f;
    }

    public void CloseDialogBox()
    {
        _background.LeanAlpha(0, 0.5f);
        _dialogBox.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInBack().setOnComplete(OnComplete);
        _audioSource.PlayOneShot(_dialogBoxCloseSound);
    }

    private void OnStartOpenDialog(){
        _audioSource.PlayOneShot(_dialogBoxOpenSound);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}