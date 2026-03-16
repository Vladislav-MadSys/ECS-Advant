using System;
using Leopotam.EcsLite;
using UnityEngine;

public sealed class EcsGameStartup : MonoBehaviour
{
    [SerializeField] private BusinessConfig[] businessConfigs;
    
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
            .Add(new IncomingSystem())
            .Add(new BusinesInitSystem(businessConfigs));
        
        _systems.Init();
    }

    private void CreatePlayer()
    {
        int entity = _world.NewEntity();    
        EcsPool<PlayerBalanceComponent> pool = _world.GetPool<PlayerBalanceComponent>();
        
        ref PlayerBalanceComponent balance = ref pool.Add(entity);
        balance.balance = 0;
    }
    
    private void TEST_CreateBuisness()
    {
        

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
