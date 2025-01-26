using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image fadeOverlay;

    [Header("Fade")]
    [SerializeField] float smootDampVelocity = 0;
    [SerializeField] float fadeThrshold = 0.005f;

    float currentAlpha = 0;
    const float MAX_ALPHA = 1.0f;
    const float MIN_ALPHA = 0f;

    public UnityAction OnFadeInFinish;
    public UnityAction OnFadeOutFinish;

    float fadeDuration;

    bool isFading = false;
    bool doFadeIn = false;
    bool doFadeOut = false;

    public bool IsFading {  get { return isFading; } }
    public bool IsFadingIn {  get { return doFadeIn; } }
    public bool IsFadingOut {  get { return doFadeOut; } }

    private void Start()
    {
        fadeOverlay = GetComponent<Image>();
    }

    private void Update()
    {
        if (doFadeIn)
        {
            currentAlpha = Mathf.SmoothDamp(currentAlpha, MIN_ALPHA, ref smootDampVelocity, fadeDuration);

            Color c = fadeOverlay.color;
            c.a = currentAlpha;
            fadeOverlay.color = c;

            if (Mathf.Abs(MIN_ALPHA - currentAlpha) <= fadeThrshold)
            {
                Debug.Log("Fade In Finished");
                OnFadeInFinish?.Invoke();
                doFadeIn = false;
                isFading = false;
            }
        }
        if (doFadeOut)
        {
            currentAlpha = Mathf.SmoothDamp(currentAlpha, MAX_ALPHA, ref smootDampVelocity, fadeDuration);

            Color c = fadeOverlay.color;
            c.a = currentAlpha;
            fadeOverlay.color = c;

            if (Mathf.Abs(MAX_ALPHA - currentAlpha) <= fadeThrshold)
            {
                Debug.Log("Fade Out Finished");
                OnFadeOutFinish?.Invoke();
                doFadeOut = false;
                isFading = false;
            }
        }
    }

    public void PlayFadeIn(float duration)
    {
        Debug.Log("Start Fade In");
        currentAlpha = MAX_ALPHA;
        fadeDuration = duration;
        doFadeIn = true;
        isFading = true;
    }

    public void PlayFadeOut(float duration)
    {
        Debug.Log("Start Fade Out");
        currentAlpha = MIN_ALPHA;
        fadeDuration = duration;
        doFadeOut = true;
        isFading = true;
    }
}
