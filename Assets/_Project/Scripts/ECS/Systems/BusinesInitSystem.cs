using Leopotam.EcsLite;
using UnityEngine;

public class BusinesInitSystem : IEcsInitSystem
{
    private BusinessConfig[] _configs;
    private EcsWorld _world;
    
    public BusinesInitSystem(BusinessConfig[] configs)
    {
        _configs = configs;
    }
    
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        foreach (var config in _configs)
        {
            int entity = _world.NewEntity();
            EcsPool<BusinessComponent> buisnessPool = _world.GetPool<BusinessComponent>();
            EcsPool<IncomeProgressComponent> incomeProgressPool = _world.GetPool<IncomeProgressComponent>();
        
            ref BusinessComponent businessComponent = ref buisnessPool.Add(entity);
            ref IncomeProgressComponent incomeProgressComponent = ref incomeProgressPool.Add(entity);

            businessComponent.name = config.name;
            businessComponent.level = config.level;
            businessComponent.basicIncoming = config.basicIncoming;
            businessComponent.incomintDelay = config.incomintDelay;

            incomeProgressComponent.incomintProgress = 0;
        
            Debug.Log($"Business {businessComponent.name} created!");
        }
    }
}
