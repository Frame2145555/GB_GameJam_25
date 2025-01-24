using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BattleModeManager : MonoBehaviour
{
    static BattleModeManager instance;

    [Header("Reference")]
    [SerializeField] GameObject map;

    bool isInBattle;
    bool isBattleModeActive;

    public UnityAction OnBattleModeEnter;

    public bool IsBattleModeActive { get => isBattleModeActive; }
    public static BattleModeManager Instance { get => instance; }
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EnterBattleMode()
    {
        isBattleModeActive = true;
        OnBattleModeEnter?.Invoke();

        map.SetActive(true);
    }
}
