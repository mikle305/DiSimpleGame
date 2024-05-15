using UnityEngine;

namespace SimpleGame.Services
{
    public class AssetsProvider
    {
        public T[] LoadAll<T>(string name)
            where T : Object
            => Resources.LoadAll<T>(name);
    }
}