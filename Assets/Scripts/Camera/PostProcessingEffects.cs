using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffects : MonoBehaviour
{
    public PostProcessVolume volume;

    private Bloom bloom;
    private Vignette vignette;

    private float defaultBloomValue;
    private float defaultVignetteValue;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out vignette);
        defaultBloomValue = bloom.intensity.value;
        defaultVignetteValue = vignette.intensity.value;
    }

    public IEnumerator flashScreen() {
        bloom.intensity.value = 40f;
        yield return new WaitForSeconds(0.2f);
        bloom.intensity.value = defaultBloomValue;
    }
}
