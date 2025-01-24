using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;


public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;

    [Header("Character Animator")]  
    [SerializeField] List<Pair<string,AnimatorController>> animControllersWithTag;

    bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.Instance.DialogueIsPlaying)
        {
            if (InputManager.Instance.IsInteractKeyDown)
            {
                DialogueManager.Instance.EnterDialogueMode(inkJSON,animControllersWithTag);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
