using Cysharp.Threading.Tasks;
using SimpleGame.Services;
using UniDependencyInjection;
using UniDependencyInjection.Core;
using UniDependencyInjection.Unity;
using UnityEngine;

namespace SimpleGame.GameFlow
{
    public class ContainerBootstrap : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private GameObject[] _autoInject = {};
        
        private IMonoResolver _container;

        
        private void Awake()
        {
            _container = CreateContainer();
            InjectInSceneObjects();
        }

        private void Start()
        {
            _container
                .CreateScope()
                .Resolve<EntryPoint>()
                .Execute().Forget();
        }

        private IMonoResolver CreateContainer()
        {
            IContainerBuilder containerBuilder = new ContainerBuilder();
            RegisterServices(containerBuilder);
            
            return containerBuilder.Build() as IMonoResolver;
        }

        private void RegisterServices(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterSingle<EntryPoint>();
            containerBuilder.RegisterSingle<AssetsProvider>();
            containerBuilder.RegisterSingle<ConfigsProvider>();
            containerBuilder.RegisterSingle<ProgressService>();
            containerBuilder.RegisterSingle(_loadingCurtain);
            containerBuilder.RegisterSingle<GameService>();
        }

        private void InjectInSceneObjects()
        {
            foreach (GameObject obj in _autoInject) 
                _container.Inject(obj);
        }
    }
}