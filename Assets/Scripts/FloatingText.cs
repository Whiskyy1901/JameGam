using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float rise = 120f;
    public float life = 0.8f;
    public float popScale = 1.3f;

    TMP_Text txt;
    float t;

    void Awake() => txt = GetComponent<TMP_Text>();
    public void Init(string s) => txt.text = s;

    void Update()
    {
        t += Time.deltaTime;
        float k = t / life;
        transform.position += Vector3.up * (rise * Time.deltaTime);
        transform.localScale = Vector3.one * Mathf.Lerp(popScale, 1f, Mathf.Min(1f, k * 4f));
        txt.alpha = 1f - k * k;
        if (t >= life) Destroy(gameObject);
    }
}