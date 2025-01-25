using UnityEngine;

public enum CameraMode
{
    Static,
    ClampDynamic
}

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [Header("Tuning")]
    [SerializeField] int teleportBackTimes = 3;
    int teleportBackCount = 0;
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
        
    }
}
