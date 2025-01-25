using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class PostProcessingControlFrame : MonoBehaviour
{
    [SerializeField] private Volume postProcessingVolume;
    private Vignette vignette;

    private void Start()
    {
        if (postProcessingVolume != null && postProcessingVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    public IEnumerator BlinkEffect()
    {
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(0f, 1f, time / duration); // เพิ่มความเข้ม
            yield return null;
        }
    }

    public IEnumerator RemoveBlinkEffect()
    {
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }
    }
}
