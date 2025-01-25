using UnityEngine;
using UnityEngine.Events;

public abstract class concreteBullet:MonoBehaviour
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
    }

    public virtual void PatternEnd()
    {
        Debug.Log("Pattern ended");
        this.enabled = false;
    }

}

