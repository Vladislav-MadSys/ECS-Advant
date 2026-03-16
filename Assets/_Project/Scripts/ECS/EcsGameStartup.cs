using System;
using Leopotam.EcsLite;
using UnityEngine;

public sealed class EcsGameStartup : MonoBehaviour
{
    private EcsWorld world;
    private EcsSystems systems;

    private void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);
        
        AddInjections();
        AddOneFrames();
        AddSystems();
        
        TEST_CreatePlayer();
        TEST_CreateBuisness();

        systems
            .Add(new IncomingSystem());
        
        systems.Init();
    }

    private void TEST_CreatePlayer()
    {
        int entity = world.NewEntity();    
        EcsPool<PlayerBalanceComponent> pool = world.GetPool<PlayerBalanceComponent>();
        
        ref PlayerBalanceComponent balance = ref pool.Add(entity);
        balance.balance = 0;
    }
    
    private void TEST_CreateBuisness()
    {
        int entity = world.NewEntity();
        EcsPool<BusinessComponent> buisnessPool = world.GetPool<BusinessComponent>();
        EcsPool<IncomeProgressComponent> incomeProgressPool = world.GetPool<IncomeProgressComponent>();
        
        ref BusinessComponent businessComponent = ref buisnessPool.Add(entity);
        ref IncomeProgressComponent incomeProgressComponent = ref incomeProgressPool.Add(entity);

        businessComponent.name = $"Business {entity}";
        businessComponent.level = 1;
        businessComponent.currentIncoming = 1;
        businessComponent.incomintDelay = 5;

        incomeProgressComponent.incomintProgress = 0;
        
        Debug.Log($"Business {businessComponent.name} created!");

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
        systems.Run();
    }

    private void OnDestroy()
    {
        systems.Destroy();
        systems = null;
        world.Destroy();
    }
}
