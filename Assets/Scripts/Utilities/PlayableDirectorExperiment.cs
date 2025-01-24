using UnityEngine;
using UnityEngine.Playables;

public class PlayableDirectorExperiment : MonoBehaviour
{
    [SerializeField] PlayableDirector pd;

    private void Start()
    {
        pd.stopped += OnPDEnded;
    }

    void OnPDEnded(PlayableDirector pd)
    {
        if (pd == this.pd)
        {
            Debug.Log("Timeline has finished");
        }
    }
}
