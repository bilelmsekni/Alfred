using System;
using Alfred.Dal.Implementation.Fake.Standard.Dao;
using Alfred.Dal.Implementation.Fake.Standard.Mappers;
using Alfred.Dal.Standard.Daos;
using Alfred.Dal.Standard.Mappers;
using Alfred.Dal.Standard.Repositories;
using Alfred.Domain.Standard.Repositories;
using Alfred.Domain.Standard.Services;
using Alfred.Domain.Standard.Validators;
using Alfred.Shared.Standard.Features;
using Alfred.Standard.Models.Artifacts;
using Alfred.Standard.Models.Communities;
using Alfred.Standard.Models.Members;
using Alfred.Standard.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Alfred.IoC.Standard
{
    public static class Bootstrap
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IArtifactService, ArtifactService>();
            serviceCollection.AddTransient<IMemberService, MemberService>();
            serviceCollection.AddTransient<ICommunityService, CommunityService>();
            serviceCollection.AddTransient<IArtifactRepository, ArtifactRepository>();
            serviceCollection.AddTransient<IMemberRepository, MemberRepository>();
            serviceCollection.AddTransient<ICommunityRepository, CommunityRepository>();
            serviceCollection.AddTransient<IArtifactDao, ArtifactDao>();
            serviceCollection.AddTransient<IMemberDao, MemberDao>();
            serviceCollection.AddTransient<ICommunityDao, CommunityDao>();
            serviceCollection.AddSingleton<ObjectDifferenceManager>(new ObjectDifferenceManager());
            serviceCollection.AddTransient<IEntityFactory, EntityFactory>();
            serviceCollection.AddTransient<IModelFactory, ModelFactory>();
            serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            serviceCollection.AddSingleton<AbstractValidator<ArtifactCriteriaModel>>(new ArtifactCriteriaModelValidator(new IdsValidator()));
            serviceCollection.AddSingleton<AbstractValidator<CommunityCriteriaModel>>(new CommunityCriteriaModelValidator(new IdsValidator()));
            serviceCollection.AddSingleton<AbstractValidator<MemberCriteriaModel>>(new MemberCriteriaModelValidator(new IdsValidator()));
            return serviceCollection;
        }
    }
}
