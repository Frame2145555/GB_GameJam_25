using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public enum CameraMode
{
    Static,
    FollowRight,
    ClampDynamic
}

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    Fade fade;

    [Header("Game")]
    [SerializeField] Transform player;

    [SerializeField] int teleportBackTimes = 3;
    int teleportBackCount = 0;

    [Header("Camera Handle")]
    CameraMode cameraMode = CameraMode.Static;

    [SerializeField] Transform cameraHolder;
    [SerializeField] MinMaxNum<float> clampDynamicRegion;

    [Header("Vignette Handle")]
    [SerializeField] PostProcessVolume postProcessingVolume;

    [Range(0.0f, 1.0f)]
    [SerializeField] float intensity;

    Vignette vignette;


    public static GameManager Instance { get => instance; }
    public int TeleportBackCount { get => teleportBackCount; set => teleportBackCount = value; }
    public bool IsInStartLoop() => teleportBackCount < teleportBackTimes;

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

    }

    private void Update()
    {
        HandleCameraState();
    }

    void HandleCameraState()
    {
        if (IsInStartLoop()) return;

        if (player.position.x > cameraHolder.position.x && player.position.x < clampDynamicRegion.min)
            cameraMode = CameraMode.FollowRight;
        else if (player.position.x > clampDynamicRegion.min && player.position.x < clampDynamicRegion.max)
            cameraMode = CameraMode.ClampDynamic;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 init = Vector2.right * clampDynamicRegion.min + Vector2.up * -10;
        Vector2 end = Vector2.right * clampDynamicRegion.max + Vector2.up * -10;
        Gizmos.DrawLine(init, end);
    }

}
