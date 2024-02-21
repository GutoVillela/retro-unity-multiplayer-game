using TMPro;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    [SerializeField] private AudioClip _skillSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TMP_Text _skillText;

    public void ShowSkill(string skillText)
{
        gameObject.SetActive(true);
        _skillText.SetText(skillText);
    }


    void OnEnable()
    {
        _skillText.transform.localScale = Vector3.zero;
        _skillText.gameObject.LeanAlpha(0, .1f).setEaseInExpo();
        _skillText.gameObject.LeanScale(Vector3.one, .3f).setEaseInExpo().setOnComplete(OnCompleteFadeIn);
    }

    void OnCompleteFadeIn()
    {
        _audioSource.PlayOneShot(_skillSound);
        _skillText.gameObject.LeanScale(Vector3.zero, .5f).setEaseOutExpo().setOnComplete(OnCompleteFadeOut).delay = 4.5f;
    }

    void OnCompleteFadeOut()
    {
        gameObject.SetActive(false);
    }
}