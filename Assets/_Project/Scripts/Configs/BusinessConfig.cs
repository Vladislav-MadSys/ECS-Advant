using UnityEngine;

[CreateAssetMenu(menuName = "Business", fileName = "BusinessConfig")]
public class BusinessConfig : ScriptableObject
{
    public string name;
    public int level;
    public int basicPrice;
    public int basicIncoming;
    public float incomintDelay;
    public int firstUpgradePrise;
    public float firstUpgradeIncomingMultiplier;
    public int secondUpgradePrise;
    public float secondUpgradeIncomingMultiplier;
}
