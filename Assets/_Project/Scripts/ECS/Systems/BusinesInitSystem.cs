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

                businessComponent.name = config.name;
                businessComponent.level = config.level;
                businessComponent.basicIncoming = config.basicIncoming;
                businessComponent.incomintDelay = config.incomintDelay;

                incomeProgressComponent.incomintProgress = 0;
            
                var panel = Object.Instantiate(_businessPanelPrefab, _businessPanelParent).GetComponent<BusinessView>();
                panel.SetBusinessName(config.name);
                panel.SetLevelText(config.level.ToString());
                panel.SetIncomingText(config.basicIncoming.ToString());
                businessViewRef.View = panel;
            
                Debug.Log($"Business {businessComponent.name} created!");
            }
        }
    }
}
