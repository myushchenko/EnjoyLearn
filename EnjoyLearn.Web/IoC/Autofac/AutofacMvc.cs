using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using EnjoyLearn.Data;
using EnjoyLearn.Data.Core;
using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Services;
using EnjoyLearn.Services.Interfaces;
using NLog.Mvc;

namespace EnjoyLearn.Web.IoC.Autofac
{
  internal static class AutofacMvc
  {
    public static void Initialize()
    {
      var builder = new ContainerBuilder();
      DependencyResolver.SetResolver(new AutofacDependencyResolver(RegisterServices(builder)));
    }

    private static IContainer RegisterServices(ContainerBuilder builder)
    {
      builder.RegisterControllers(typeof(MvcApplication).Assembly);
      builder.RegisterType<Logger>().As<ILogger>();
      builder.RegisterType<EnjoyLearnContext>().As<IEntitiesContext>();
      builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>));
      builder.RegisterType<UserService>().As<IUserService>();
      return builder.Build();
    }
  }
}