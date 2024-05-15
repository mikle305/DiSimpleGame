using System;
using System.Collections.Generic;
using System.Linq;
using SimpleGame.Additional;
using Object = UnityEngine.Object;

namespace SimpleGame.Services
{
    public class ConfigsProvider
    {
        private readonly AssetsProvider _assetsProvider;
        private readonly Dictionary<Type, Object> _configsMap;
        
        
        public ConfigsProvider(AssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
            _configsMap = new Dictionary<Type, Object>();
        }
        
        public void LoadConfig<T>() 
            where T : Object
        {
            var config = _assetsProvider.LoadAll<T>(Constants.ConfigsLabel).Single();
            _configsMap[typeof(T)] = config;
        }

        public T GetConfig<T>() 
            where T : Object 
            => _configsMap[typeof(T)] as T;
    }
}