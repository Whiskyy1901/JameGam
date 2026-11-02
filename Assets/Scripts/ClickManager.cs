using UnityEngine;
using TMPro;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ClickManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text counterText;
    public TMP_Text upgradeButtonText;
    public TMP_Text helperButtonText;
    public TMP_Text perSecondText;

    [Header("Click upgrade")]
    public long mass = 0;
    public long massPerClick = 1;
    public long upgradeCost = 10;
    public int upgradeLevel = 0;
    public float costMultiplier = 3f;
    public int maxLevel = 6;

    [Header("Helpers (passive income)")]
    public long helperCost = 50;
    public int helperCount = 0;
    public float helperCostMultiplier = 1.4f;
    float passiveBuffer;

    [Header("References")]
    public GrowthStages growth;
    public EndingSequence ending;
    public SoundManager sound;
    public SquashStretch buttonSquash;
    public SquashStretch guySquash;
    public SquashStretch upgradeSquash;
    public SquashStretch helperSquash;  // optional

    [Header("Juice")]
    public CameraShake shake;
    public PostFXPulse postFX;          // optional
    public FloatingText popupPrefab;
    public Transform popupParent;       // the Canvas

    public long PerSecond => helperCount * System.Math.Max(1L, massPerClick / 4);

    void Start() => UpdateUI();

    void Update()
    {
        if (ending != null && ending.Played) return;
        if (helperCount <= 0) return;

        passiveBuffer += PerSecond * Time.deltaTime;
        if (passiveBuffer >= 1f)
        {
            long whole = (long)passiveBuffer;
            passiveBuffer -= whole;
            mass += whole;
            UpdateUI();
        }
    }

    public void OnClick()
    {
        if (ending != null && ending.Played) return;
        mass += massPerClick;
        Wiggle();
        sound.PlayClick();
        SpawnPopup("+" + Format(massPerClick));
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (mass < upgradeCost) { sound.PlayError(); return; }
        mass -= upgradeCost;
        upgradeLevel++;
        massPerClick *= 2;
        upgradeCost = (long)(upgradeCost * costMultiplier);

        if (upgradeSquash != null) upgradeSquash.Punch();
        sound.PlayUpgrade(upgradeLevel);
        if (shake != null) shake.Shake(0.3f, 0.15f + upgradeLevel * 0.03f);
        if (postFX != null) postFX.Pulse();

        UpdateUI();
        growth.Apply(upgradeLevel);

        if (upgradeLevel >= maxLevel) ending.Play();
    }

    public void BuyHelper()
    {
        if (mass < helperCost) { sound.PlayError(); return; }
        mass -= helperCost;
        helperCount++;
        helperCost = (long)(helperCost * helperCostMultiplier) + 1;

        if (helperSquash != null) helperSquash.Punch();
        sound.PlayUpgrade(Mathf.Min(upgradeLevel, 8));
        UpdateUI();
    }

    public void UpdateUI()
    {
        counterText.text = "Mass: " + Format(mass);
        upgradeButtonText.text = "EXPAND (Cost: " + Format(upgradeCost) + ")";
        if (helperButtonText != null)
            helperButtonText.text = "HELPER x" + helperCount + " (Cost: " + Format(helperCost) + ")";
        if (perSecondText != null)
            perSecondText.text = PerSecond > 0 ? Format(PerSecond) + "/s" : "";
    }

    public void Wiggle()
    {
//        buttonSquash.Punch();
        guySquash.Punch();
    }

    void SpawnPopup(string text)
    {
        if (popupPrefab == null || popupParent == null) return;
        Vector3 pos = MousePos() + new Vector3(Random.Range(-30f, 30f), Random.Range(0f, 30f), 0f);
        var p = Instantiate(popupPrefab, pos, Quaternion.identity, popupParent);
        p.Init(text);
    }

    static Vector3 MousePos()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector3.zero;
#endif
    }

    public static string Format(long n)
    {
        double v = n;
        if (v >= 1e12) return (v / 1e12).ToString("0.##") + "T";
        if (v >= 1e9) return (v / 1e9).ToString("0.##") + "B";
        if (v >= 1e6) return (v / 1e6).ToString("0.##") + "M";
        if (v >= 1e4) return (v / 1e3).ToString("0.#") + "K";
        return n.ToString();
    }
}