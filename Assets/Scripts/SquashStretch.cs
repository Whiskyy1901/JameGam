using UnityEngine;

public class SquashStretch : MonoBehaviour
{
    public bool applyToSelf = true;
    public float squash = 0.25f;
    public float speed = 14f;
    public float damping = 7f;

    [Header("Randomness")]
    [Range(0f, 1f)] public float strengthVariation = 0.4f;
    [Range(0f, 1f)] public float speedVariation = 0.15f;
    [Range(0f, 1f)] public float stretchChance = 0.2f;

    public Vector3 Multiplier { get; private set; } = Vector3.one;
    
    float offset, velocity, currentSpeed;
    Vector3 baseScale;

    void Awake()
    {
        baseScale = transform.localScale;
        currentSpeed = speed;
    }

    public void Punch()
    {
        float strength = squash * Random.Range(1f - strengthVariation, 1f + strengthVariation);
        if (Random.value < stretchChance) strength = -strength;
        currentSpeed = speed * Random.Range(1f - speedVariation, 1f + speedVariation);
        velocity = -strength * currentSpeed;
    }

    void Update()
    {
        velocity += -offset * currentSpeed * currentSpeed * Time.deltaTime;
        velocity *= Mathf.Exp(-damping * Time.deltaTime);
        offset += velocity * Time.deltaTime;

        Multiplier = new Vector3(1f - offset, 1f + offset, 1f);

        if (applyToSelf)
            transform.localScale = Vector3.Scale(baseScale, Multiplier);
    }
}