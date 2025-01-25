using System.Collections.Generic;
using UnityEngine;

public class BattleModePatternStore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject patternHolder;
    [SerializeField] List<ConcreteBulletPattern> bulletPatterns = new List<ConcreteBulletPattern>();

    int currentPatternIndex = -1;
    public void NextPattern()
    {
        if (currentPatternIndex + 1 >= bulletPatterns.Count)
        {
            Debug.LogWarning("No more Pattern left to play : Index = " + (currentPatternIndex + 1));
        }
        
        currentPatternIndex++;

        Debug.Log("Played Pattern #" + currentPatternIndex);

        // bulletPatterns[currentPatternIndex].enabled = true;

    }

}

internal class ConcreteBulletPattern : MonoBehaviour
{ 

}
