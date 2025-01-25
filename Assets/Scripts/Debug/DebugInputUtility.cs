using UnityEngine;

public class DebugInputUtility : MonoBehaviour
{
    BattleModeDialogueStore bmds;
    Fade f;

    [SerializeField] float fadeDuration = 5;
    private void Start()
    {
        bmds = FindAnyObjectByType<BattleModeDialogueStore>();
        f = FindAnyObjectByType<Fade>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            f.PlayFadeIn(fadeDuration);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            f.PlayFadeOut(fadeDuration);
        }
    }


}
