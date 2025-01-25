using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeleportTrigger : MonoBehaviour
{
    [Header("Teleport To Position")]
    [SerializeField] Vector3 target = Vector3.zero;

    [Header("Handle Fade")]
    [SerializeField] float fadeInTime = 0.2f;
    [SerializeField] float fadeOutTime = 0.5f;

    Fade fade;

    UnityEvent onFadeFinished = new UnityEvent();

    private void Start()
    {
        fade = FindAnyObjectByType<Fade>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.IsInStartLoop())
        {
            fade.OnFadeInFinish += SendFadeNotify;
            fade.OnFadeOutFinish += SendFadeNotify;

            GameManager.Instance.TeleportBackCount++;
            StartCoroutine(TriggerTeleportEvent(collision.gameObject));

        }
    }

    IEnumerator TriggerTeleportEvent(GameObject player)
    {
        fade.PlayFadeOut(fadeOutTime);

        bool isFadeOutFinished = false;
        onFadeFinished.AddListener(() => isFadeOutFinished = true);
        yield return new WaitUntil(() => isFadeOutFinished);

        player.transform.position = target;
        fade.PlayFadeIn(fadeInTime);

        onFadeFinished.RemoveAllListeners();
        fade.OnFadeInFinish -= SendFadeNotify;
        fade.OnFadeOutFinish -= SendFadeNotify;
    }

    void SendFadeNotify()
    {
        onFadeFinished?.Invoke();
    }
}
