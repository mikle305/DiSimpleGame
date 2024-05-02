using System;
using System.Collections.Generic;

namespace Examples.ServiceLocator
{
public static class ServiceLocator
{
    private static Dictionary<Type, object> _services;
    
    public static void Initialize()
        => _services = new Dictionary<Type, object>();

    public static void Register<T>(T service) 
        => _services[typeof(T)] = service;

    public static T Resolve<T>() 
        => (T)_services[typeof(T)];

    public static void Release()
        => _services = null;
}

public class GamePlay
{
    private AudioManager _audioManager;

    public GamePlay()
    {
        _audioManager = ServiceLocator.Resolve<AudioManager>();
    }

    public void Execute()
    {
        _audioManager.PlayMusic("SomeMusic");
    }
}

public class EntryPoint
{
    public static void Main()
    {
        // Регистрация
        ServiceLocator.Initialize();
        ServiceLocator.Register(new AudioManager());
        ServiceLocator.Register(new GamePlay());
        
        // Разрешение
        ServiceLocator.Resolve<GamePlay>().Execute();
        
        // Освобождение
        ServiceLocator.Release();
    }
}

public class AudioManager
{
    public void PlayMusic(string musicName)
    {
        // Проигрывает музыку под названием musicName
    }
}
}