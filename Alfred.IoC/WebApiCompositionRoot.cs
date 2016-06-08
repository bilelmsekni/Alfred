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
            serviceRegistry.Register<IMemberRepository, MemberRepository>();
            serviceRegistry.Register<IMemberDao, MemberDao>();
            serviceRegistry.Register<IModelFactory, ModelFactory>();            
        }
    }
}
