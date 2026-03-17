using System;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BusinessView : MonoBehaviour
    {
        [Header("Basic Data")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI incomingText;
        [SerializeField] private Image incomingBar;
    
        [Header("Level Upgrade")]
        [SerializeField] private TextMeshProUGUI levelUpPriceText;
        [SerializeField] private Button levelUpButton;
    
        [Header("First Upgrade")]
        [SerializeField] private TextMeshProUGUI firstUpgradeIncomingText;
        [SerializeField] private TextMeshProUGUI firstUpgradePriceText;
        [SerializeField] private Button firstUpgradeButton;
    
        [Header("Second Upgrade")]
        [SerializeField] private TextMeshProUGUI secondUpgradeIncomingText;
        [SerializeField] private TextMeshProUGUI secondUpgradePriceText;
        [SerializeField] private Button secondUpgradeButton;

        private EcsWorld _world;
        private int _businessEntity;

        private UnityAction _onLevelUpClicked;
        private UnityAction _onFirstUpgradeClicked;
        private UnityAction _onSecondUpgradeClicked;
        
        private void Start()
        {
            _onLevelUpClicked = () =>
            {
                LevelUpClicked();  
            };
            levelUpButton.onClick.AddListener(_onLevelUpClicked);

            _onFirstUpgradeClicked = () =>
            {
                UpgradeClicked(0);
            };
            firstUpgradeButton.onClick.AddListener(_onFirstUpgradeClicked);

            _onSecondUpgradeClicked = () =>
            {
                UpgradeClicked(1);
            };
            secondUpgradeButton.onClick.AddListener(_onSecondUpgradeClicked);
        }

        private void OnDestroy()
        {
            levelUpButton.onClick.RemoveListener(_onLevelUpClicked);
            firstUpgradeButton.onClick.RemoveListener(_onFirstUpgradeClicked);
            secondUpgradeButton.onClick.RemoveListener(_onSecondUpgradeClicked);
        }

        public void Init(EcsWorld world, int businessEntity)
        {
            _world = world;
            _businessEntity = businessEntity;
        }

        public void SetBusinessName(string newName)
        {
            nameText.text = '"' + newName + '"';
        }
    
        public void SetLevelText(string newLevelText)
        {
            levelText.text = newLevelText;
        }

        public void SetLevelUpPrice(int levelUpPrice)
        {
            levelUpPriceText.text = levelUpPrice + "$";
        }

        public void SetFirstUpgradePrice(int upgradePrice, bool isBuyed = false)
        {
            firstUpgradePriceText.text = isBuyed == false ? upgradePrice + "$" : "Куплено";
        }
        
        public void SetSecondUpgradePrice(int upgradePrice, bool isBuyed = false)
        {
            secondUpgradePriceText.text = isBuyed == false ? upgradePrice + "$" : "Куплено";
        }

        public void SetFirstUpgradeIncomingMultiplier(float multiplier)
        {
            firstUpgradeIncomingText.text = "Доход + " + (multiplier*100) + "%";
        }
        
        public void SetSecondUpgradeIncomingMultiplier(float multiplier)
        {
            secondUpgradeIncomingText.text = "Доход + " + (multiplier*100) + "%";
        }

        public void SetIncomingText(string newIncomingText)
        {
            incomingText.text = newIncomingText;
        }

        public void UpdateBar(float incomeProgress, float incomeDelay)
        {
            incomingBar.fillAmount = incomeProgress / incomeDelay;
        }

        private void LevelUpClicked()
        {
            var entity = _world.NewEntity();
            var pool = _world.GetPool<BuyLevelRequest>();
            ref var request = ref pool.Add(entity);
            request.businessEntity = _businessEntity;
        }

        private void UpgradeClicked(int upgradeId)
        {
            var entity = _world.NewEntity();
            var pool = _world.GetPool<BuyUpgradeRequest>();
            ref var request = ref pool.Add(entity);
            request.businessEntity = _businessEntity;
            request.upgradeId = upgradeId;
        }

    }
}
