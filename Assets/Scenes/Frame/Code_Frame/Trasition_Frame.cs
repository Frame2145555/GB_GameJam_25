using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Trasition_Frame : MonoBehaviour
{
    [SerializeField] private Animator animatorTransition;

    [SerializeField] private float transitionTime;

    [Header("Teleport Settings")]
    [SerializeField] private Transform targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("WarpToPoint")) 
        {
            StartCoroutine(LoadLevel());
        }
    }

    private void TeleportToTarget()
    {
        if (targetPosition != null)
        {
            transform.position = targetPosition.position;
            Debug.Log("Teleported to: " + targetPosition.position);
        }
        else
        {
            Debug.LogWarning("Target position is not set!");
        }
    }

    IEnumerator LoadLevel()
    {
        animatorTransition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        TeleportToTarget();
    }
    
}
