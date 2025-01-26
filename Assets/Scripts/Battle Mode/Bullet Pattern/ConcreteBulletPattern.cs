using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public abstract class ConcreteBulletPattern : MonoBehaviour
{
    public UnityAction OnPatternStart;
    public UnityAction OnPatternEnd;

    [SerializeField] ControllerMode controllerMode = ControllerMode.Topdown;

    public ControllerMode GetControllerMode() => controllerMode;

    private void OnEnable()
    {
        PatternStart();
    }

    protected virtual void Update()
    {
        BattleModeManager.Instance.IsInBattle = true;
    }

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

