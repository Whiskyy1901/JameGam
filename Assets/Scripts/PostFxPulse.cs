using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostFXPulse : MonoBehaviour
{
    public Volume volume;
    public float bloomBase = 0.5f, bloomPeak = 6f;
    public float aberrationPeak = 0.8f;
    public float decay = 4f;

    Bloom bloom;
    ChromaticAberration aberration;
    float pulse;

    void Start()
    {
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out aberration);
        Apply();
    }

    public void Pulse() => pulse = 1f;

    void Update()
    {
        if (pulse <= 0f) return;
        pulse = Mathf.MoveTowards(pulse, 0f, decay * Time.deltaTime);
        Apply();
    }

    void Apply()
    {
        if (bloom != null) bloom.intensity.value = Mathf.Lerp(bloomBase, bloomPeak, pulse);
        if (aberration != null) aberration.intensity.value = aberrationPeak * pulse;
    }
}