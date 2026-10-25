using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrowthStages : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        public string name = "Stage";
        public int startLevel;
        public float guyScale = 1f;
        public float camSize = 5f;

        [Header("Visuals (leave empty to skip)")]
        public Sprite guySprite;
        public Color guyColor = Color.white;
        public Sprite backgroundSprite;
        public Color backgroundColor = Color.white;
        public Color panelColor = Color.white;
        public Color textColor = Color.black;
    }

    [Header("Scene references")]
    public Transform guy;
    public SpriteRenderer guyRenderer;
    public Camera cam;
    public SpriteRenderer backgroundRenderer;
    public TMP_Text stageText;
    public TMP_Text[] allTexts;
    public Image[] panelImages;

    public Stage[] stages;
    public float lerpSpeed = 2f;

    float targetScale, targetCam;

    void Start() => Apply(0);

    public void Apply(int upgradeLevel)
    {
        int index = 0;
        for (int i = 0; i < stages.Length; i++)
            if (upgradeLevel >= stages[i].startLevel) index = i;

        Stage s = stages[index];
        targetScale = s.guyScale;
        targetCam = s.camSize;

        if (s.guySprite != null) guyRenderer.sprite = s.guySprite;
        guyRenderer.color = s.guyColor;

        cam.backgroundColor = s.backgroundColor;
        if (backgroundRenderer != null)
        {
            if (s.backgroundSprite != null) backgroundRenderer.sprite = s.backgroundSprite;
            backgroundRenderer.color = s.backgroundColor;
        }

        foreach (var img in panelImages) if (img != null) img.color = s.panelColor;
        foreach (var t in allTexts) if (t != null) t.color = s.textColor;

        if (stageText != null) stageText.text = s.name;
    }

    void Update()
    {
        guy.localScale = Vector3.Lerp(guy.localScale, Vector3.one * targetScale, Time.deltaTime * lerpSpeed);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetCam, Time.deltaTime * lerpSpeed);
    }
}