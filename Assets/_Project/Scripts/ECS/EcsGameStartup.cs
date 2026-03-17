using _Project.Scripts.Configs;
using _Project.Scripts.ECS.Components;
using _Project.Scripts.ECS.Systems;
using _Project.Scripts.UI;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.ECS
{
    public sealed class EcsGameStartup : MonoBehaviour
    {
        [Header("Gameplay UI")]
        [SerializeField] private GameplayView _gameplayView;
    
        [Header("Business")]
        [SerializeField] private BusinessConfig[] businessConfigs;
        [SerializeField] private GameObject businessPanelPrefab;
        [SerializeField] private Transform businessPanelParent;
    
    
        private EcsWorld _world;
        private EcsSystems _systems;

        private int _playerBalanceEntity;
        
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
        
            CreatePlayer();

            _systems
                .Add(new IncomingSystem(_gameplayView, _playerBalanceEntity))
                .Add(new BusinesInitSystem(businessConfigs, businessPanelPrefab, businessPanelParent))
                .Add(new BusinessUISystem())
                .Add(new BusinessUpgradeSystem(_playerBalanceEntity, _gameplayView));
        
            _systems.Init();
        }

        private void CreatePlayer()
        {
            _playerBalanceEntity = _world.NewEntity();
            EcsPool<PlayerBalanceComponent> pool = _world.GetPool<PlayerBalanceComponent>();
            ref PlayerBalanceComponent balance = ref pool.Add(_playerBalanceEntity);
            balance.Balance = 0;
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
        }
    }
}
