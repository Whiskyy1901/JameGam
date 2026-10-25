using UnityEngine;
using TMPro;

public class ClickManager : MonoBehaviour
{
    public TMP_Text counterText;
    public int mass = 0;
    public int massPerClick = 1;
    
    public void OnClick()
    {
        mass += massPerClick;
        UpdateUI();
    }

    public void UpdateUI()
    {
        counterText.text = "Mass: " + mass.ToString("F0");
    }

    void Start() => UpdateUI();
}
