using System.Reflection;
using Alfred.Configuration;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Domain.Implementation;
using Alfred.Domain.Mappers;
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
