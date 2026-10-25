using UnityEngine;
using TMPro;

public class GrowthStages : MonoBehaviour
{
    public Transform guy;
    public SpriteRenderer guyRenderer;
    public Camera cam;
    public TMP_Text stageText;

    public int[] levelThresholds = { 0, 2, 3, 5, 6 };
    public string[] stageNames = { "Little Guy", "Building", "Planet", "Star", "Black Hole" };
    public float[] guyScales = { 1f, 3f, 8f, 20f, 40f };
    public float[] camSizes = { 5f, 10f, 25f, 60f, 120f };
    public Color[] bgColors = {
        new Color(0.95f, 0.95f, 0.95f), new Color(0.5f, 0.7f, 1f),
        new Color(0.1f, 0.2f, 0.5f), new Color(0.05f, 0.05f, 0.2f), Color.black };
    public Color[] guyColors = {
        Color.red, Color.gray, Color.green, Color.yellow, Color.black };

    float targetScale, targetCam;

    void Start() => Apply(0);

    public void Apply(int upgradeLevel)
    {
        int stage = 0;
        for (int i = 0; i < levelThresholds.Length; i++)
            if (upgradeLevel >= levelThresholds[i]) stage = i;

        targetScale = guyScales[stage];
        targetCam = camSizes[stage];
        cam.backgroundColor = bgColors[stage];
        guyRenderer.color = guyColors[stage];
        stageText.text = stageNames[stage];
    }

    void Update()
    {
        guy.localScale = Vector3.Lerp(guy.localScale, Vector3.one * targetScale, Time.deltaTime * 2f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetCam, Time.deltaTime * 2f);
    }
}