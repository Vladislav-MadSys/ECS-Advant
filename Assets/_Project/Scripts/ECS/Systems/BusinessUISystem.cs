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
                
                businessViewRef.View.UpdateBar(incomeComponent.IncomintProgress, businessComponent.IncomintDelay);
                businessViewRef.View.SetIncomingText(businessComponent.CurrentIncoming.ToString());
                businessViewRef.View.SetLevelText(businessComponent.Level.ToString());
                businessViewRef.View.SetLevelUpPrice((businessComponent.Level+1)*businessComponent.BasicLevelUpCost);
                businessViewRef.View.SetFirstUpgradePrice(businessComponent.FirstUpgradePrice, businessComponent.IsFirstUpgradeBuyed);
                businessViewRef.View.SetSecondUpgradePrice(businessComponent.SecondUpgradePrice, businessComponent.IsSecondUpgradeBuyed);
                businessViewRef.View.SetFirstUpgradeIncomingMultiplier(businessComponent.FirstUpgradeMultipler);
                businessViewRef.View.SetSecondUpgradeIncomingMultiplier(businessComponent.SecondUpgradeMultipler);
            }
        }
    }
}
