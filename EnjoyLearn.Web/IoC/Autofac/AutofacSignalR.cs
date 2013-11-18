using Autofac;
using Autofac.Integration.SignalR;
using EnjoyLearn.Data;
using EnjoyLearn.Data.Core;
using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Services;
using EnjoyLearn.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using NLog.Mvc;
using System.Reflection;

namespace EnjoyLearn.Web.IoC.Autofac
{
  public static class AutofacSignalR
  {
    public static void Initialize()
    {
      GlobalHost.DependencyResolver = new AutofacDependencyResolver(RegisterServices(new ContainerBuilder()));
    }

    private static IContainer RegisterServices(ContainerBuilder builder)
    {
      builder.RegisterHubs(Assembly.GetExecutingAssembly());
      builder.RegisterType<Logger>().As<ILogger>();
      builder.RegisterType<EnjoyLearnContext>().As<IEntitiesContext>();
      builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>));
      builder.RegisterType<UserService>().As<IUserService>();
      return builder.Build();
    }
  }
}