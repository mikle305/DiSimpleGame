using UniDependencyInjection.Unity;
using UnityEngine;

namespace Examples.DiContainer
{
    public class SomeComponent : MonoBehaviour
    {
        private AudioManager _audioManager;

        
        [Inject]
        public void Construct(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
    }
}