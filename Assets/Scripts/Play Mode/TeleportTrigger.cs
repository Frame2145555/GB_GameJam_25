using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeleportTrigger : MonoBehaviour
{
    Transform cameraTransform;
    [Header("Teleport To Position")]
    [SerializeField] Transform target;
    [SerializeField] Transform cameraTarget;

    [Header("Handle Fade")]
    [SerializeField] float fadeInTime = 0.2f;
    [SerializeField] float fadeOutTime = 0.5f;
     
    Fade fade;

    UnityEvent onFadeFinished = new UnityEvent();

    private int countLoop = 0;

    private void Start()
    {
        fade = FindAnyObjectByType<Fade>();
        cameraTransform = Camera.main.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fade.OnFadeInFinish += SendFadeNotify;
            fade.OnFadeOutFinish += SendFadeNotify;
            StartCoroutine(TriggerTeleportEvent(collision.gameObject));

            countLoop++;

              
        }
    }

    IEnumerator TriggerTeleportEvent(GameObject player)
    {
        fade.PlayFadeOut(fadeOutTime);

        bool isFadeOutFinished = false;
        onFadeFinished.AddListener(() => isFadeOutFinished = true);
        yield return new WaitUntil(() => isFadeOutFinished);

        player.transform.position = target.position;
        cameraTransform.position = cameraTarget.position;
        fade.PlayFadeIn(fadeInTime);

        onFadeFinished.RemoveAllListeners();
        fade.OnFadeInFinish -= SendFadeNotify;
        fade.OnFadeOutFinish -= SendFadeNotify;
    }

    void SendFadeNotify()
    {
        onFadeFinished?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector2 TL = new Vector3(-6.22f*2, 3.5f*2)  + cameraTarget.position;
        Vector2 TR = new Vector3(6.22f * 2, 3.5f * 2)   + cameraTarget.position;
        Vector2 BL = new Vector3(-6.22f * 2, -3.5f * 2) + cameraTarget.position;
        Vector2 BR = new Vector3(6.22f * 2, -3.5f * 2)  + cameraTarget.position;

        Gizmos.DrawLine(TL, TR);
        Gizmos.DrawLine(TR, BR);
        Gizmos.DrawLine(BR, BL);
        Gizmos.DrawLine(BL, TL);
    }
}
