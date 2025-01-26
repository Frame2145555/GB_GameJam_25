using Ink.Parsed;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class BattleModeDialogueStore : MonoBehaviour
{
    [Header("Ink JSON Store")]
    [SerializeField] List<TextAsset> inkJSON_s;

    [Header("Character Animator Store")]
    [SerializeField] List<InnerList<Pair<string, AnimatorController>>> animControllersWithTags;


    int dialogueIndex = -1;

    public int DialogueIndex { get => dialogueIndex; set => dialogueIndex = value; }

    private void Start()
    {
        BattleModeManager.Instance.OnBattleModeEnter += InitializeBattleModeDialogueStore;
    }

    void InitializeBattleModeDialogueStore()
    {
        dialogueIndex = -1;
    }

    public void NextDialogue()
    {
        if (dialogueIndex + 1 >= inkJSON_s.Count)
        {
            Debug.LogWarning("No more dialogue left to be play");
            return;
        }
        else if (BattleModeManager.Instance.IsInBattle || DialogueManager.Instance.DialogueIsPlaying)
            return;

        dialogueIndex++;

        DialogueManager.Instance.EnterDialogueMode(
            inkJSON_s[dialogueIndex],
            animControllersWithTags[dialogueIndex].innerList);

        DialogueManager.Instance.OnDialogueExit += OnDialogueExit;
    }

    void OnDialogueExit()
    {
        BattleModeManager.Instance.NextBattlePhase();
        DialogueManager.Instance.OnDialogueExit -= OnDialogueExit;
    }
}
