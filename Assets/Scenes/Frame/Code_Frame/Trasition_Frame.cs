using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Trasition_Frame : MonoBehaviour
{
    [SerializeField] private Animator animatorTransition;
    [SerializeField] private float transitionTime = 1f;

    [Header("Teleport Settings")]
    [SerializeField] private Transform targetPosition;

    private PostProcessingControlFrame postProcessingControlFrame;

    private void Start()
    {
        postProcessingControlFrame = FindObjectOfType<PostProcessingControlFrame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleportSequence(collision.gameObject));
        }
    }

    private IEnumerator TeleportSequence(GameObject objectToTeleport)
    {
        if (postProcessingControlFrame != null)
        {
            StartCoroutine(postProcessingControlFrame.BlinkEffect());
        }

        animatorTransition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        TeleportToTarget(objectToTeleport);

        if (postProcessingControlFrame != null)
        {
            StartCoroutine(postProcessingControlFrame.RemoveBlinkEffect());
        }
    }

    private void TeleportToTarget(GameObject objectToTeleport)
    {
        if (targetPosition != null)
        {
            objectToTeleport.transform.position = targetPosition.position;
            Debug.Log($"{objectToTeleport.name} teleported to: {targetPosition.position}");
        }
        else
        {
            Debug.LogWarning("Target position is not set!");
        }
    }
}
