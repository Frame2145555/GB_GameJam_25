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

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 init = Vector2.right * clampDynamicRegion.min + Vector2.up * -10;
        Vector2 end = Vector2.right * clampDynamicRegion.max + Vector2.up * -10;
        Gizmos.DrawLine(init, end);
    }

}
