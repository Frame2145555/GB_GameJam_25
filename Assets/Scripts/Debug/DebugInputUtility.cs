using UnityEngine;

public class DebugInputUtility : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.Instance.IsInteractKeyDown && !BattleModeManager.Instance.IsBattleModeActive)
        {
            BattleModeManager.Instance.EnterBattleMode();
        }
    }
}
