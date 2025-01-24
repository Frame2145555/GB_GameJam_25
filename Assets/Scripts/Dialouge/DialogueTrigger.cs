using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DialougeTriggerMode
{
    OnInteract,
    OnEnter
}
public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Mode")]
    [SerializeField] DialougeTriggerMode mode = DialougeTriggerMode.OnEnter;

    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;

    [Header("Character Animator")]  
    [SerializeField] List<Pair<string,AnimatorController>> animControllersWithTag;

    bool playerInRange;
    bool isPlayed = false;

    private void Awake()
    {
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.Instance.DialogueIsPlaying && !isPlayed)
        {
            switch (mode)
            {
                case DialougeTriggerMode.OnInteract:
                    if (InputManager.Instance.IsInteractKeyDown)
                        StartDialouge();
                    break;
                case DialougeTriggerMode.OnEnter:
                    StartDialouge();
                    break;

            }
        }
    }

    private void StartDialouge() 
    {
        DialogueManager.Instance.EnterDialogueMode(inkJSON, animControllersWithTag);
        isPlayed = true;
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

    public void SetPlayable(bool value) => isPlayed = value;
    
}
