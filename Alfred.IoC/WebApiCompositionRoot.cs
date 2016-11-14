using System;
using System.Reflection;
using Alfred.Configuration.Json;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Model;
using Alfred.Model.Implementation;
using LightInject;

namespace Alfred.IoC
{
    public class WebApiCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterAssembly(Assembly.GetAssembly(typeof(IMemberDao)));
            serviceRegistry.Register<IModelFactory, ModelFactory>();
            serviceRegistry.RegisterInstance(AppSettingsProvider.Build());
        }
    }
}
