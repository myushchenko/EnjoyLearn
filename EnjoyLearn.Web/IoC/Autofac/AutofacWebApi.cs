using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using EnjoyLearn.Data;
using EnjoyLearn.Data.Core;
using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Services;
using EnjoyLearn.Services.Interfaces;
using NLog.Mvc;

namespace EnjoyLearn.Web.IoC.Autofac
{
  public static class AutofacWebApi
  {
    public static void Initialize(HttpConfiguration config)
    {
      Initialize(config, RegisterServices(new ContainerBuilder()));
    }

    public static void Initialize(HttpConfiguration config, IContainer container)
    {
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }

    private static IContainer RegisterServices(ContainerBuilder builder)
    {
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
      builder.RegisterType<Logger>().As<ILogger>();
      builder.RegisterType<EnjoyLearnContext>().As<IEntitiesContext>();
      builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>));
      builder.RegisterType<UserService>().As<IUserService>();
      return builder.Build();
    }
  }
}