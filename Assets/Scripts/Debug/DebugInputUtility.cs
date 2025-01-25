using UnityEngine;

public class DebugInputUtility : MonoBehaviour
{
    BattleModeDialogueStore bmds;
    private void Start()
    {
        bmds = FindAnyObjectByType<BattleModeDialogueStore>();
    }
    private void Update()
    {
        if (InputManager.Instance.IsInteractKeyDown && !BattleModeManager.Instance.IsBattleModeActive)
        {
            BattleModeManager.Instance.EnterBattleMode();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            bmds.NextDialogue();
        }
    }


}
