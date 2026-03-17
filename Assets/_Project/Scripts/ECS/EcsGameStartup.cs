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
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
        
            AddInjections();
            AddOneFrames();
            AddSystems();
        
            CreatePlayer();

            _systems
                .Add(new IncomingSystem(_gameplayView))
                .Add(new BusinesInitSystem(businessConfigs, businessPanelPrefab, businessPanelParent))
                .Add(new BusinessUISystem());
        
            _systems.Init();
        }

        private void CreatePlayer()
        {
            int entity = _world.NewEntity();    
            EcsPool<PlayerBalanceComponent> pool = _world.GetPool<PlayerBalanceComponent>();
        
            ref PlayerBalanceComponent balance = ref pool.Add(entity);
            balance.balance = 0;
        }
    
        private void AddSystems()
        {
        
        }

        private void AddInjections()
        {
        
        }

        private void AddOneFrames()
        {
        
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
