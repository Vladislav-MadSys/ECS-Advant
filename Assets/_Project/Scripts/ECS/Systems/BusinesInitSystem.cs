using _Project.Scripts.Configs;
using _Project.Scripts.ECS.Components;
using _Project.Scripts.UI;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.ECS.Systems
{
    public class BusinesInitSystem : IEcsInitSystem
    {
        private BusinessConfig[] _configs;
        private EcsWorld _world;
        private GameObject _businessPanelPrefab;
        private Transform _businessPanelParent;
    
        public BusinesInitSystem(BusinessConfig[] configs, GameObject businessPanelPrefab, Transform businessPanelParent)
        {
            _configs = configs;
            _businessPanelPrefab = businessPanelPrefab;
            _businessPanelParent = businessPanelParent;
        }
    
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            foreach (var config in _configs)
            {
                int entity = _world.NewEntity();
                EcsPool<BusinessComponent> buisnessPool = _world.GetPool<BusinessComponent>();
                EcsPool<IncomeProgressComponent> incomeProgressPool = _world.GetPool<IncomeProgressComponent>();
                EcsPool<BusinessViewRef> viewRef =  _world.GetPool<BusinessViewRef>();
        
                ref BusinessComponent businessComponent = ref buisnessPool.Add(entity);
                ref IncomeProgressComponent incomeProgressComponent = ref incomeProgressPool.Add(entity);
                ref BusinessViewRef businessViewRef = ref viewRef.Add(entity);

                businessComponent.Name = config.name;
                businessComponent.Level = config.level;
                businessComponent.BasicIncoming = config.basicIncoming;
                businessComponent.CurrentIncoming = config.basicIncoming;
                businessComponent.IncomintDelay = config.incomintDelay;
                businessComponent.BasicLevelUpCost = config.basicPrice;
                businessComponent.IsFirstUpgradeBuyed = false;
                businessComponent.IsSecondUpgradeBuyed = false;
                businessComponent.FirstUpgradePrice = config.firstUpgradePrice;
                businessComponent.SecondUpgradePrice = config.secondUpgradePrice;
                businessComponent.FirstUpgradeMultipler = config.firstUpgradeIncomingMultiplier;
                businessComponent.SecondUpgradeMultipler = config.secondUpgradeIncomingMultiplier;

                incomeProgressComponent.IncomintProgress = 0;
            
                var panel = Object.Instantiate(_businessPanelPrefab, _businessPanelParent).GetComponent<BusinessView>();
                panel.Init(_world, entity);
                panel.SetBusinessName(config.name);
                panel.SetLevelText(config.level.ToString());
                panel.SetIncomingText(config.basicIncoming.ToString());
                businessViewRef.View = panel;
            }
        }
    }
}
