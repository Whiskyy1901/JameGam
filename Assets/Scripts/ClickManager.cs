using UnityEngine;
using TMPro;

public class ClickManager : MonoBehaviour
{
    public TMP_Text counterText;
    public TMP_Text upgradeButtonText;
    public int mass = 0;
    public int massPerClick = 1;
    public int upgradeCost = 10;
    public int upgradeLevel = 0;

    public GrowthStages growth;
    public EndingSequence ending;
    public SoundManager sound;
    
    public void OnClick()
    {
        mass += massPerClick;
        sound.PlayClick();
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if (mass < upgradeCost) { sound.PlayError(); return; }

        sound.PlayUpgrade();
        mass -= upgradeCost;
        upgradeLevel++;
        massPerClick *= 2;
        upgradeCost *= 3;
        UpdateUI();
        growth.Apply(upgradeLevel);
        if (upgradeLevel >= 6) ending.Play();
    }

    public void UpdateUI()
    {
        counterText.text = "Mass: " + mass.ToString("F0");
        upgradeButtonText.text = "EXPAND (Cost: " + upgradeCost.ToString("F0") + ")";
    }

    void Start() => UpdateUI();
}
