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
        
        systems.Init();
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
