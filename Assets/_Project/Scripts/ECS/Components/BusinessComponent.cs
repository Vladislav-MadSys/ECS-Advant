namespace _Project.Scripts.ECS.Components
{
    public struct BusinessComponent
    {
        public string Name;
        public int Level;
        public int BasicIncoming;
        public int CurrentIncoming;
        public float IncomintDelay;
        public int BasicLevelUpCost;

        public bool IsFirstUpgradeBuyed;
        public int FirstUpgradePrice;
        public float FirstUpgradeMultipler;
        
        public bool IsSecondUpgradeBuyed;
        public int SecondUpgradePrice; 
        public float SecondUpgradeMultipler;
    }
}
