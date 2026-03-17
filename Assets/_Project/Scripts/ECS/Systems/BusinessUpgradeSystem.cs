
using _Project.Scripts.ECS.Components;
using _Project.Scripts.UI;
using Leopotam.EcsLite;

public class BusinessUpgradeSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world = null;

    private EcsFilter _businessFilter;
    private EcsFilter _levelUpFilter;
    private EcsFilter _upgradeFilter;

    private EcsPool<PlayerBalanceComponent> _playerBalancePool;
    private EcsPool<BusinessComponent> _businessPool;
    private EcsPool<BuyLevelRequest> _buyLevelRequestPool;
    private EcsPool<BuyUpgradeRequest> _buyUpgradeRequestPool;

    private GameplayView _gameplayView;
    private int _playerBalance;

    public BusinessUpgradeSystem(int playerBalance, GameplayView gameplayView)
    {
        _gameplayView = gameplayView;
        _playerBalance = playerBalance;
    }
    
    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        
        _businessFilter = _world.Filter<BusinessComponent>().End();
        _businessPool = _world.GetPool<BusinessComponent>();
        
        _levelUpFilter = _world.Filter<BuyLevelRequest>().End();
        _buyLevelRequestPool = _world.GetPool<BuyLevelRequest>();
        
        _upgradeFilter = _world.Filter<BuyUpgradeRequest>().End();
        _buyUpgradeRequestPool = _world.GetPool<BuyUpgradeRequest>();
        
        _playerBalancePool = _world.GetPool<PlayerBalanceComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        
        ref PlayerBalanceComponent balance = ref _playerBalancePool.Get(_playerBalance);
        
        foreach (var entity in _levelUpFilter)
        {
            ref BuyLevelRequest buyLevelRequest = ref _buyLevelRequestPool.Get(entity);
            ref BusinessComponent businessComponent = ref _businessPool.Get(buyLevelRequest.businessEntity);
            
            int businessLevelUpCost = (businessComponent.Level + 1) * businessComponent.BasicLevelUpCost;
            if (businessLevelUpCost <= balance.Balance)
            {
                balance.Balance -= businessLevelUpCost;
                businessComponent.Level++;
                _gameplayView.UpdateBalance(balance.Balance.ToString());
            }
            
            int profit = (int)(businessComponent.Level * businessComponent.BasicIncoming*
                               (1  
                                + (businessComponent.IsFirstUpgradeBuyed ? businessComponent.FirstUpgradeMultipler : 0) 
                                + (businessComponent.IsSecondUpgradeBuyed ? businessComponent.SecondUpgradeMultipler : 0)));
            businessComponent.CurrentIncoming = profit;
            
            _buyLevelRequestPool.Del(entity);
        }

        foreach (var entity in _upgradeFilter)
        {
            ref BuyUpgradeRequest buyUpgradeRequest = ref _buyUpgradeRequestPool.Get(entity);
            ref BusinessComponent businessComponent = ref _businessPool.Get(buyUpgradeRequest.businessEntity);

            int businessUpgradePrice;
            if (buyUpgradeRequest.upgradeId == 0)
            {
                businessUpgradePrice = businessComponent.FirstUpgradePrice;
                if (businessUpgradePrice <= balance.Balance && businessComponent.IsFirstUpgradeBuyed == false)
                {
                    balance.Balance -= businessUpgradePrice;
                    businessComponent.IsFirstUpgradeBuyed = true;  
                }
            }
            else if (buyUpgradeRequest.upgradeId == 1)
            {
                businessUpgradePrice = businessComponent.SecondUpgradePrice;
                if (businessUpgradePrice <= balance.Balance && businessComponent.IsSecondUpgradeBuyed == false)
                {
                    balance.Balance -= businessUpgradePrice;
                    businessComponent.IsSecondUpgradeBuyed = true;
                }
            }
            _gameplayView.UpdateBalance(balance.Balance.ToString());
            
            int profit = (int)(businessComponent.Level * businessComponent.BasicIncoming*
                               (1  
                                + (businessComponent.IsFirstUpgradeBuyed ? businessComponent.FirstUpgradeMultipler : 0) 
                                + (businessComponent.IsSecondUpgradeBuyed ? businessComponent.SecondUpgradeMultipler : 0)));
            businessComponent.CurrentIncoming = profit;
            
            _buyUpgradeRequestPool.Del(entity);
        }
    }
}
