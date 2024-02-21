using TMPro;
using UnityEngine;

public class CriticalTextEffect : MonoBehaviour
{
    [SerializeField] private AudioClip _criticalSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TMP_Text _criticalText;

    public void ShowCriticalText()
    {
        gameObject.SetActive(true);
    }


    void OnEnable()
    {
        //_criticalText.SetText("Crítico!");
        _criticalText.transform.localScale = Vector3.zero;
        _criticalText.gameObject.LeanAlpha(0, .1f).setEaseInExpo();
        _criticalText.gameObject.LeanScale(Vector3.one, .3f).setEaseInExpo().setOnComplete(OnCompleteFadeIn);
    }

    void OnCompleteFadeIn()
    {
        _audioSource.PlayOneShot(_criticalSound);
        _criticalText.gameObject.LeanScale(Vector3.zero, .5f).setEaseOutExpo().setOnComplete(OnCompleteFadeOut).delay = 1f;
    }

    void OnCompleteFadeOut()
    {
        gameObject.SetActive(false);
    }
}