using TMPro;
using UnityEngine;

public class HitText : MonoBehaviour
{
    [SerializeField] private TMP_Text _hitText;

    public void ShowHitText(string hitText, Vector2 newPosition)
    {
        _hitText.SetText(hitText);
        _hitText.transform.position = newPosition;
        gameObject.SetActive(true);
    }


    void OnEnable()
    {
        _hitText.transform.localScale = Vector3.zero;
        _hitText.gameObject.LeanAlpha(0, .1f).setEaseInExpo();
        _hitText.gameObject.LeanScale(Vector3.one, .3f).setEaseInOutBounce().setOnComplete(OnCompleteFadeIn);
        _hitText.gameObject.LeanMoveLocalX(transform.position.x - 10, .3f).setEaseInExpo();
    }

    void OnCompleteFadeIn()
    {
        _hitText.gameObject.LeanScale(Vector3.zero, .5f).setEaseOutExpo().setOnComplete(OnCompleteFadeOut).delay = 1f;
    }

    void OnCompleteFadeOut()
    {
        gameObject.SetActive(false);
    }
}