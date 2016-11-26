using Alfred.Configuration;
using Alfred.Dal.Daos;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Dal.Mappers;
using Alfred.Dal.Repositories;
using Alfred.Domain.Mappers;
using Alfred.Domain.Repositories;
using Alfred.Domain.Services;
using Alfred.Domain.Validators;
using Alfred.Models;
using Alfred.Services;
using Alfred.Shared.Features;
using FluentValidation;
using LightInject;

namespace Alfred.IoC
{
    public class WebApiCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IArtifactService, ArtifactService>();
            serviceRegistry.Register<IMemberService, MemberService>();
            serviceRegistry.Register<ICommunityService, CommunityService>();
            serviceRegistry.Register<IModelFactory, ModelFactory>();
            serviceRegistry.Register<IArtifactRepository, ArtifactRepository>();
            serviceRegistry.Register<IMemberRepository, MemberRepository>();
            serviceRegistry.Register<ICommunityRepository, CommunityRepository>();
            serviceRegistry.Register<IArtifactDao, ArtifactDao>();
            serviceRegistry.Register<IMemberDao, MemberDao>();
            serviceRegistry.Register<ICommunityDao, CommunityDao>();
            serviceRegistry.Register<IEntityFactory, EntityFactory>();
            serviceRegistry.RegisterInstance(AppSettingsProvider.Build());
            serviceRegistry.RegisterInstance<ObjectDifferenceManager>(new ObjectDifferenceManager());
            serviceRegistry.RegisterInstance<AbstractValidator<ArtifactCriteriaModel>>(new ArtifactCriteriaModelValidator(new IdsValidator()));
        }
    }
}
