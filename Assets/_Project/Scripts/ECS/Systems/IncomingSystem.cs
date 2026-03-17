using _Project.Scripts.ECS.Components;
using _Project.Scripts.UI;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.ECS.Systems
{
    public class IncomingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
    
        private EcsFilter _buisnessFilter = null;
        private EcsPool<BusinessComponent> _buisnessComponents = null;
        private EcsPool<IncomeProgressComponent> _incomeProgressComponents = null;

        private EcsFilter _playerFilter = null;
        private EcsPool<PlayerBalanceComponent> _playerBalanceComponents = null;

        private GameplayView _gameplayView;

        public IncomingSystem(GameplayView gameplayView)
        {
            _gameplayView = gameplayView;
        }
    
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        
            _buisnessFilter = _world.Filter<BusinessComponent>().Inc<IncomeProgressComponent>().End();
            _buisnessComponents = _world.GetPool<BusinessComponent>();
            _incomeProgressComponents = _world.GetPool<IncomeProgressComponent>();
        
            _playerFilter = _world.Filter<PlayerBalanceComponent>().End();
            _playerBalanceComponents = _world.GetPool<PlayerBalanceComponent>();

        }
    
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _buisnessFilter)
            {
                ref BusinessComponent business = ref _buisnessComponents.Get(entity);
                ref IncomeProgressComponent incomeProgress = ref _incomeProgressComponents.Get(entity);

                if (business.level > 0)
                {
                    if (incomeProgress.incomintProgress >= business.incomintDelay)
                    {
                        foreach (int playerBalance in _playerFilter)
                        {
                            ref PlayerBalanceComponent balance = ref _playerBalanceComponents.Get(playerBalance);
                            balance.balance += business.level * business.basicIncoming;
                            _gameplayView.UpdateBalance(balance.balance.ToString());
                            Debug.Log($"Доход начислен: +{business.basicIncoming}, баланс: {balance.balance}");
                        }

                        incomeProgress.incomintProgress = 0;
                    
                    }
                    else
                    {
                        incomeProgress.incomintProgress += Time.deltaTime;
                    }
                }
            }
        }
    }
}
