using System;
using System.Collections.Generic;
using System.Text;
using Forge.Client.Services;
using LightInject;

namespace Forge.Client
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IClientWrapper, ClientWrapper>(new PerScopeLifetime());
            serviceRegistry.Register<IBucketService, BucketService>();
        }
    }
}
