using Alfred.Dal.FakeImplementation.Dao;
using Alfred.Dal.FakeImplementation.Repositories;
using Alfred.Dal.Interfaces;
using LightInject;

namespace Alfred.IoC
{
    public class WebApiCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IMemberRepository, MemberRepository>();
            serviceRegistry.Register<IMemberDao, MemberDao>();            
        }
    }
}
