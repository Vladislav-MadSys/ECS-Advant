using Leopotam.EcsLite;
using UnityEngine;

public class IncomingSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world = null;
    
    private EcsFilter _buisnessFilter = null;
    private EcsPool<BuisnessComponent> _buisnessComponents = null;
    private EcsPool<IncomeProgressComponent> _incomeProgressComponents = null;

    private EcsFilter _playerFilter = null;
    private EcsPool<PlayerBalanceComponent> _playerBalanceComponents = null;
    
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        
        _buisnessFilter = _world.Filter<BuisnessComponent>().Inc<IncomeProgressComponent>().End();
        _buisnessComponents = _world.GetPool<BuisnessComponent>();
        _incomeProgressComponents = _world.GetPool<IncomeProgressComponent>();
        
        _playerFilter = _world.Filter<PlayerBalanceComponent>().End();
        _playerBalanceComponents = _world.GetPool<PlayerBalanceComponent>();
    }
    
    public void Run(IEcsSystems systems)
    {
        foreach (int entity in _buisnessFilter)
        {
            ref BuisnessComponent buisness = ref _buisnessComponents.Get(entity);
            ref IncomeProgressComponent incomeProgress = ref _incomeProgressComponents.Get(entity);

            if (incomeProgress.incomintProgress >= buisness.incomintDelay)
            {
                foreach (int playerBalance in _playerFilter)
                {
                    ref PlayerBalanceComponent balance = ref _playerBalanceComponents.Get(playerBalance);
                    balance.balance += buisness.currentIncoming;
                }
            }
        }
    }
}
