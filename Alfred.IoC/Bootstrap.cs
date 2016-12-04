using System;
using System.Web.Http;
using Alfred.Configuration;
using Alfred.Dal.Daos;
using Alfred.Dal.Implementation.Fake.Dao;
using Alfred.Dal.Implementation.Fake.Mappers;
using Alfred.Dal.Mappers;
using Alfred.Dal.Repositories;
using Alfred.Domain.Repositories;
using Alfred.Domain.Services;
using Alfred.Domain.Validators;
using Alfred.IoC.Handlers;
using Alfred.Models.Artifacts;
using Alfred.Models.Communities;
using Alfred.Models.Members;
using Alfred.Services;
using Alfred.Shared.Features;
using FluentValidation;
using LightInject;

namespace Alfred.IoC
{
    public static class Bootstrap
    {
        public static ServiceContainer CreateContainer(HttpConfiguration config)
        {
            return new ServiceContainer()
                .TrackHttpRequestMessage(config)
                .RegisterAppDependencies();
        }

        private static ServiceContainer TrackHttpRequestMessage(this ServiceContainer serviceContainer, HttpConfiguration config)
        {
            var handler = new HttpRequestMessageHandler();
            config.MessageHandlers.Insert(0, handler);
            serviceContainer.Register<Func<System.Net.Http.HttpRequestMessage>>(factory => () => handler.GetCurrentMessage());
            return serviceContainer;
        }

        private static ServiceContainer RegisterAppDependencies(this ServiceContainer container)
        {
            container.Register<IArtifactService, ArtifactService>();
            container.Register<IMemberService, MemberService>();
            container.Register<ICommunityService, CommunityService>();
            container.Register<IArtifactRepository, ArtifactRepository>();
            container.Register<IMemberRepository, MemberRepository>();
            container.Register<ICommunityRepository, CommunityRepository>();
            container.Register<IArtifactDao, ArtifactDao>();
            container.Register<IMemberDao, MemberDao>();
            container.Register<ICommunityDao, CommunityDao>();
            container.RegisterInstance<ObjectDifferenceManager>(new ObjectDifferenceManager());            
            container.Register<IEntityFactory, EntityFactory>();
            container.Register<IModelFactory, ModelFactory>();
            container.RegisterInstance(AppSettingsProvider.Build());
            container.RegisterInstance<AbstractValidator<ArtifactCriteriaModel>>(new ArtifactCriteriaModelValidator(new IdsValidator()));
            container.RegisterInstance<AbstractValidator<CommunityCriteriaModel>>(new CommunityCriteriaModelValidator(new IdsValidator()));
            container.RegisterInstance<AbstractValidator<MemberCriteriaModel>>(new MemberCriteriaModelValidator(new IdsValidator()));
            return container;
        }
    }
}
