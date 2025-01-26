using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

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
    Health playerHP;
    Fade fade;
    GameObject playmodePlayer;
    GameObject battlemodePlayer;

    [SerializeField] GameObject map;
    [SerializeField] Transform battleCameraPosition;
    [SerializeField] Transform lostCameraPosition;
    [SerializeField] Transform lostPlayerSpawnPosition;
    [SerializeField] Vector2 battlePlayerStartPosition;

    [Header("BattleModeTimeline")]
    [SerializeField] List<BattleModeMode> battleModeTimeline;

    bool gameWon = false;
    bool gameLost = false;

    bool isInBattle;
    bool isBattleModeActive;

    int currentBattleIndex = -1;

    public UnityAction OnBattleModeEnter;

    public UnityAction OnBattleStart;
    public UnityAction OnBattleEnd;
    private float fadeDuration;

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
    private void OnEnable()
    {
        playerHP = FindAnyObjectByType<Health>();
        playerHP.OnDead += GameLost;
    }
    private void OnDisable()
    {
        playerHP.OnDead -= GameLost;
    }
    void Start()
    {
        bmds = GetComponent<BattleModeDialogueStore>();
        bmps = GetComponent<BattleModePatternStore>();

        fade = FindAnyObjectByType<Fade>();
        playmodePlayer = FindAnyObjectByType<PlayModeController>().gameObject;
        battlemodePlayer = FindAnyObjectByType<BattleModeController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBattleModeActive)
            return;

        if (isInBattle || DialogueManager.Instance.DialogueIsPlaying)
            return;
    }
    
    public void NextBattlePhase()
    {
        if (!isBattleModeActive)
            return;

        if (currentBattleIndex + 1 >= battleModeTimeline.Count)
        {
            Debug.Log("Game Victory");
            GameVictory();
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

        battlemodePlayer.transform.position = battlePlayerStartPosition;

        Camera.main.transform.position = battleCameraPosition.position;

        map.SetActive(true);
        battlemodePlayer.SetActive(true);
        battlemodePlayer.transform.position = battlePlayerStartPosition;
        playmodePlayer.SetActive(false);

        gameWon = false;
        gameLost = false;

        NextBattlePhase();
    }

    public void GameVictory()
    {
        gameWon= true;
        fade.PlayFadeOut(fadeDuration);
        Invoke(nameof(PlayFadeIn),3);
        fade.OnFadeInFinish+= OnFadeInFinishedWon;
    }

    void OnFadeInFinishedWon()
    {
        //warp camera to end dialogue;
        fade.OnFadeInFinish-= OnFadeInFinishedWon;
    }
    void PlayFadeIn() => fade.PlayFadeIn(fadeDuration);

    public void GameLost()
    {
        gameLost = true;
        playmodePlayer.transform.position = lostPlayerSpawnPosition.position;
        battlemodePlayer.transform.position = battlePlayerStartPosition;
        fade.PlayFadeOut(fadeDuration);
        bmps.ForceEnd();
        playerHP.Reset();
        map.SetActive(false);
        battlemodePlayer.SetActive(false);
        Invoke(nameof(PlayFadeIn), 3);
        fade.OnFadeInFinish += OnFadeInFinishedLost;
        ExitBattleMode();
    }

    void OnFadeInFinishedLost()
    {
        //Set activate playmode player
        playmodePlayer.gameObject.SetActive(true);

        //Force End Pattern

        //Disablemap

        //Reset its health calss
        fade.OnFadeInFinish -= OnFadeInFinishedLost;

        //Disable BattlePLayer

        Camera.main.transform.position = lostCameraPosition.transform.position;

    }

    public void ExitBattleMode()
    {
        currentBattleIndex = -1;
        bmds.DialogueIndex = -1;
        bmps.CurrentPatternIndex = -1;
        isBattleModeActive = false;
    }
}
