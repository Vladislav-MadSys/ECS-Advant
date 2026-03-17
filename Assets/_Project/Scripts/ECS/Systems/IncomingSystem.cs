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

        private EcsPool<PlayerBalanceComponent> _playerBalanceComponents = null;

        private GameplayView _gameplayView;
        private int _playerBalanceEntity;

        public IncomingSystem(GameplayView gameplayView, int playerBalanceEntity)
        {
            _gameplayView = gameplayView;
            _playerBalanceEntity = playerBalanceEntity;
        }
    
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        
            _buisnessFilter = _world.Filter<BusinessComponent>().Inc<IncomeProgressComponent>().End();
            _buisnessComponents = _world.GetPool<BusinessComponent>();
            _incomeProgressComponents = _world.GetPool<IncomeProgressComponent>();
        
            _playerBalanceComponents = _world.GetPool<PlayerBalanceComponent>();

        }
    
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _buisnessFilter)
            {
                ref BusinessComponent business = ref _buisnessComponents.Get(entity);
                ref IncomeProgressComponent incomeProgress = ref _incomeProgressComponents.Get(entity);

                if (business.Level > 0)
                {
                    if (incomeProgress.IncomintProgress >= business.IncomintDelay)
                    {
                        ref PlayerBalanceComponent balance = ref _playerBalanceComponents.Get(_playerBalanceEntity);
                        balance.Balance += business.CurrentIncoming;
                        _gameplayView.UpdateBalance(balance.Balance.ToString());
                        incomeProgress.IncomintProgress = 0;
                    }
                    else
                    {
                        incomeProgress.IncomintProgress += Time.deltaTime;
                    }
                }
            }
        }
    }
}
