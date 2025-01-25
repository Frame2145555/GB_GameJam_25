using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public enum CameraMode
{
    Static,
    FollowRight,
    ClampDynamic
}

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [Header("Handle Story Loop")]
    [SerializeField] Vector3 playerStartPosition;
    [SerializeField] int loopLeft = 2;

    [Header("Triggers")]
    [SerializeField] GameObject loopTrigger;

    [Header("Dim Overlay")]
    [SerializeField] Image dimOverlay;
    [Range(0f,1f)] float dimAlpha = 1f;

    public static GameManager Instance { get => instance; }
    public int LoopLeft { get => loopLeft; set => loopLeft = value; }
    public Vector3 PlayerStartPosition { get => playerStartPosition; }

    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    private void Start()
    {
        playerStartPosition = FindAnyObjectByType<PlayModeController>().transform.position; 

        if (dimOverlay == null) Debug.LogError("Missing Dim Overlay");

    }

    private void Update()
    {

    }

    public void SetDimOpacityPercentage(float percentage)
    {
        dimAlpha = (100-percentage)/100;
        Color c = dimOverlay.color;
        c.a = dimAlpha;
        dimOverlay.color = c;
    }

    public void DoLoopStory(Transform playerTransform)
    {
        playerTransform.position = playerStartPosition;
    }

    public void EnterBattleMode()
    {
        BattleModeManager.Instance.EnterBattleMode();
    }

    private void OnDrawGizmos()
    {

    }

}
