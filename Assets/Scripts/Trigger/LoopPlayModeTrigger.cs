using UnityEngine;

public class LoopPlayModeTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.DoLoopStory(collision.gameObject.transform);
        }
    }
}
