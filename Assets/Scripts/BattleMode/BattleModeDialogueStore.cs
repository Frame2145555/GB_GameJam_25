using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class BattleModeDialogueStore : MonoBehaviour
{
    [Header("Ink JSON Store")]
    [SerializeField] List<TextAsset> inkJSONs;

    [Header("Character Animator Store")]
    [SerializeField] List<List<Pair<string, AnimatorController>>> animControllersWithTags;

    int dialogueIndex = -1;

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
        dialogueIndex++;

        if (dialogueIndex >= inkJSONs.Count)
        {
            Debug.LogWarning("No more dialogue left to be play");
            return;
        }

        DialogueManager.Instance.EnterDialogueMode(
            inkJSONs[dialogueIndex],
            animControllersWithTags[dialogueIndex]);


    }
}
