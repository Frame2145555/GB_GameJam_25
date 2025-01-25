using UnityEngine;

public class BattleModeEnterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BattleModeManager.Instance.EnterBattleMode();
        }
    }
}
