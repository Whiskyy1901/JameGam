using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 origin;
    void Awake() => origin = transform.localPosition;

    public void Shake(float duration = 0.25f, float strength = 0.3f)
    {
        StopAllCoroutines();
        StartCoroutine(Run(duration, strength));
    }

    IEnumerator Run(float d, float s)
    {
        float t = 0;
        while (t < d)
        {
            t += Time.deltaTime;
            Vector2 r = Random.insideUnitCircle * s * (1f - t / d);
            transform.localPosition = origin + new Vector3(r.x, r.y, 0f);
            yield return null;
        }
        transform.localPosition = origin;
    }
}