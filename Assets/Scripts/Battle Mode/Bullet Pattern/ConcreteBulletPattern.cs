using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public abstract class ConcreteBulletPattern : MonoBehaviour
{
    public UnityAction OnPatternStart;
    public UnityAction OnPatternEnd;

    private void Start()
    {
        PatternStart();
    }

    protected abstract void Update();

    public virtual void PatternStart()
    {
        OnPatternStart?.Invoke();
        BattleModeManager.Instance.IsInBattle = true;
    }

    public virtual void PatternEnd()
    {
        BattleModeManager.Instance.IsInBattle = false;
        OnPatternEnd?.Invoke();
        this.enabled = false;
    }

}

