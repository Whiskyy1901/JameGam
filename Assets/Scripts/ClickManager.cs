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
    
    public void OnClick()
    {
        mass += massPerClick;
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        if(mass < upgradeCost) return;

        mass -= upgradeCost;
        upgradeLevel++;
        massPerClick *= 2;
        upgradeCost *= 3;
        UpdateUI();
    }

    public void UpdateUI()
    {
        counterText.text = "Mass: " + mass.ToString("F0");
        upgradeButtonText.text = "EXPAND (Cost: " + upgradeCost.ToString("F0") + ")";
    }

    void Start() => UpdateUI();
}
