using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public abstract class ConcreteBulletPattern : MonoBehaviour
{
    private void Start()
    {
        PatternStart();
    }

    
    protected virtual void Update()
    {
        
    }

    public virtual void PatternStart()
    {
        Debug.Log("Pattern started");
        BattleModeManager.Instance.IsInBattle = true;
    }

    public virtual void PatternEnd()
    {
        Debug.Log("Pattern ended");
        BattleModeManager.Instance.IsInBattle = false;
        this.enabled = false;
    }

}

