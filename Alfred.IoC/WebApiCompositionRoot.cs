using System.Reflection;
using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.Repositories;
using Alfred.Dal.Interfaces;
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
        }
    }
}
