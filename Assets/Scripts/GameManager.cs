using UnityEngine;

public enum CameraMode
{
    Static,
    ClampDynamic
}

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    Fade fade;

    [Header("Game")]
    [SerializeField] int teleportBackTimes = 3;
    int teleportBackCount = 0;
    public static GameManager Instance { get => instance; }
    public int TeleportBackCount { get => teleportBackCount; set => teleportBackCount = value; }

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
        fade = FindAnyObjectByType<Fade>();
    }

    private void Update()
    {
        
    }

}
