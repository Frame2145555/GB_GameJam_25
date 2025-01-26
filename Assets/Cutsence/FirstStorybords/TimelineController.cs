using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector

    void Start()
    {
        // Automatically play the Timeline on Start
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
    }

    public void PlayTimeline()
    {
        // Play the Timeline
        if (playableDirector != null)
        {
            playableDirector.Play();
            Debug.Log("Timeline is playing!");
        }
    }

    public void PauseTimeline()
    {
        // Pause the Timeline
        if (playableDirector != null)
        {
            playableDirector.Pause();
            Debug.Log("Timeline is paused.");
        }
    }

    public void StopTimeline()
    {
        // Stop the Timeline
        if (playableDirector != null)
        {
            playableDirector.Stop();
            Debug.Log("Timeline is stopped.");
        }
    }
}
