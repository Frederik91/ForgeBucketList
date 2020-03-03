using LightInject;

namespace Forge.BucketList
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<Forge.Client.CompositionRoot>();
        }
    }
}
