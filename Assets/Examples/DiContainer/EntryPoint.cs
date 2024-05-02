using UniDependencyInjection;

namespace Examples.DiContainer
{
    public class EntryPoint
    {
        public void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSingle<AudioManager>();
        }
    }
}