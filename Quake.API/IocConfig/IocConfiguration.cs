using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using Quake.Repository.QaukeContext;
using Quake.Repository.QuakeRepository;
using Quake.Toolkit.Settings;


namespace Quake.API.IocConfig
{
    public static class IoC
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }
    }
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorCompositionRoot(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(
                new Release(
                    () => _container.Release(controller)));

            return controller;
        }

        private sealed class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
    public class WindsorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_container.Kernel.HasComponent(serviceType))
            {
                return new object[0];
            }

            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            this._container = container;
            this._scope = container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            this._scope.Dispose();
        }
    }
    public class ApiControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,
        Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
             .BasedOn<ApiController>()
             .LifestylePerWebRequest());
        }
    }
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IQuakeContext>().ImplementedBy<QuakeContext>().LifeStyle.Transient);
            container.Register(Component.For<ISettingService>().ImplementedBy<WebConfigSettingService>().LifeStyle.Transient);

            //Repositories
            container.Register(Component.For<IQuakeRepository>().ImplementedBy<QuakeRepository>().LifeStyle.Transient);


            //Service Registering
            container.Register(Component.For<Service.Alarm.IAlarmService>().ImplementedBy<Service.Alarm.AlarmService>().LifeStyle.Transient);
            container.Register(Component.For<Service.Earthquakes.IEarthquakesService>().ImplementedBy<Service.Earthquakes.EarthquakesService>().LifeStyle.Transient);
            container.Register(Component.For<Service.Friends.IFriendsService>().ImplementedBy<Service.Friends.FriendsService>().LifeStyle.Transient);
            container.Register(Component.For<Service.Groups.IGroupsService>().ImplementedBy<Service.Groups.GroupsService>().LifeStyle.Transient);
            container.Register(Component.For<Service.TokenBased.ITokenService>().ImplementedBy<Service.TokenBased.TokenService>().LifeStyle.Transient);
            container.Register(Component.For<Service.Users.IUserService>().ImplementedBy<Service.Users.UserService>().LifeStyle.Transient);
            container.Register(Component.For<Service.UserAlarms.IUserAlarmsService>().ImplementedBy<Service.UserAlarms.UserAlarmsService>().LifeStyle.Transient);
            container.Register(Component.For<Service.Regions.IRegionsService>().ImplementedBy<Service.Regions.RegionsService>().LifeStyle.Transient);
            container.Register(Component.For<Service.Gift.IGiftService>().ImplementedBy<Service.Gift.GiftService>().LifeStyle.Transient);

            //Register Attribute
            //container.Register(Component.For<AccessControl>().ImplementedBy<AccessControl>().LifeStyle.Transient);
        }
    }
}