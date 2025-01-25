using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum BattleModeMode
{
    Dialogue,
    Battle,
}

public class BattleModeManager : MonoBehaviour
{
    static BattleModeManager instance;

    [Header("Reference")]
    BattleModeDialogueStore bmds;
    BattleModePatternStore bmps;

    [SerializeField] GameObject map;
    [SerializeField] Transform battleCameraPosition;

    [Header("BattleModeTimeline")]
    [SerializeField] List<BattleModeMode> battleModeTimeline;


    bool isInBattle;
    bool isBattleModeActive;

    int currentBattleIndex = -1;

    public UnityAction OnBattleModeEnter;

    public UnityAction OnBattleStart;
    public UnityAction OnBattleEnd;

    public bool IsBattleModeActive { get => isBattleModeActive; }
    public bool IsInBattle { get => isInBattle; set => isInBattle = value; }
    public static BattleModeManager Instance { get => instance;}
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
        bmds = GetComponent<BattleModeDialogueStore>();
        bmps = GetComponent<BattleModePatternStore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBattleModeActive)
            return;

        if (isInBattle || DialogueManager.Instance.DialogueIsPlaying)
            return;

        if (InputManager.Instance.IsInteractKeyDown)
            NextBattlePhase();
    }
    
    void NextBattlePhase()
    {
        if (currentBattleIndex + 1 >= battleModeTimeline.Count)
        {
            Debug.LogWarning("No more items in timeline. Index = " + currentBattleIndex);
            return;
        }

        currentBattleIndex++;

        switch (battleModeTimeline[currentBattleIndex])
        {
            case BattleModeMode.Dialogue:
                bmds.NextDialogue();
                break;
            case BattleModeMode.Battle:
                bmps.NextPattern();
                break;
            default:
                break;
        }
    }

    public void EnterBattleMode()
    {
        isBattleModeActive = true;
        OnBattleModeEnter?.Invoke();

        Camera.main.transform.position = battleCameraPosition.position;

        map.SetActive(true);
        NextBattlePhase();
    }

}
