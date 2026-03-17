using _Project.Scripts.ECS.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.ECS.Systems
{
    public class BusinessUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _filter = null;
        private EcsPool<BusinessComponent> _businessPool = null;
        private EcsPool<IncomeProgressComponent> _incomeProgressPool = null;
        private EcsPool<BusinessViewRef> _businessViewRef = null;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        
            _filter = _world.Filter<BusinessComponent>().Inc<BusinessViewRef>().Inc<IncomeProgressComponent>().End();
            _businessPool = _world.GetPool<BusinessComponent>();
            _businessViewRef = _world.GetPool<BusinessViewRef>();
            _incomeProgressPool = _world.GetPool<IncomeProgressComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                var businessComponent = _businessPool.Get(entity);
                var incomeComponent = _incomeProgressPool.Get(entity);
                var businessViewRef = _businessViewRef.Get(entity);
            
                businessViewRef.View.UpdateBar(incomeComponent.incomintProgress, businessComponent.incomintDelay);
            }
        }
    }
}
