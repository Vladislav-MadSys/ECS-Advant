using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BusinessView : MonoBehaviour
{
    [Header("Basic Data")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI incomingText;
    [SerializeField] private Image incomingBar;
    
    [Header("Level Upgrade")]
    [SerializeField] private TextMeshProUGUI levelUpPriceText;
    
    [Header("First Upgrade")]
    [SerializeField] private TextMeshProUGUI firstUpgradeIncomingText;
    [SerializeField] private TextMeshProUGUI firstUpgradePriceText;
    
    [Header("Second Upgrade")]
    [SerializeField] private TextMeshProUGUI secondUpgradeIncomingText;
    [SerializeField] private TextMeshProUGUI secondUpgradePriceText;

    public void SetBusinessName(string newName)
    {
        nameText.text = '"' + newName + '"';
    }
    
    public void SetLevelText(string newLevelText)
    {
        levelText.text = newLevelText;
    }

    public void SetIncomingText(string newIncomingText)
    {
        incomingText.text = newIncomingText;
    }

    public void UpdateBar(float incomeProgress, float incomeDelay)
    {
        incomingBar.fillAmount = incomeProgress / incomeDelay;
    }
}
