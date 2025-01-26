using System.Collections.Generic;
using UnityEngine;

public class BattleModePatternStore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject patternHolder;
    [SerializeField] List<ConcreteBulletPattern> bulletPatterns = new List<ConcreteBulletPattern>();
    [SerializeField] BattleModeController controller;

    ConcreteBulletPattern currentPattern;

    int currentPatternIndex = -1;
    public int CurrentPatternIndex { get { return currentPatternIndex; } set => currentPatternIndex = value; }
    public void NextPattern()
    {
        if (currentPatternIndex + 1 >= bulletPatterns.Count)
        {
            Debug.LogWarning("No more Pattern left to play : Index = " + (currentPatternIndex + 1));
        }
        
        currentPatternIndex++;

        currentPattern = bulletPatterns[currentPatternIndex];
        currentPattern.enabled = true;

        controller.ChangeMode(currentPattern.GetControllerMode());

        currentPattern.OnPatternEnd += OnPatternEnd;
    }

    void OnPatternEnd()
    {
        currentPattern.OnPatternEnd -= OnPatternEnd;
        currentPattern = null;
        BattleModeManager.Instance.NextBattlePhase();
    }

    public void ForceEnd()
    {
        currentPattern.OnPatternEnd = null;
        foreach(var bulletPattern in bulletPatterns)
        {
            bulletPattern.PatternEnd();
            bulletPattern.enabled = false;
        }

    }

}
