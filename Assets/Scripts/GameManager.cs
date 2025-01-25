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

    int loop;


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

    private void OnDrawGizmos()
    {

    }

}
