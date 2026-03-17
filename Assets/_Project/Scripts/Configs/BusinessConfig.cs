using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Business", fileName = "BusinessConfig")]
    public class BusinessConfig : ScriptableObject
    {
        public string name;
        public int level;
        public int basicPrice;
        public int basicIncoming;
        public float incomintDelay;
        public int firstUpgradePrice;
        public float firstUpgradeIncomingMultiplier;
        public int secondUpgradePrice;
        public float secondUpgradeIncomingMultiplier;
    }
}
