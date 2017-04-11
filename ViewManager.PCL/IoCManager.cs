using Microsoft.Practices.Unity;
using System;

namespace ViewManagement
{
    public class IoCManager
    {
        private UnityContainer _container;

        public IoCManager(UnityContainer unityContainer)
        {
            _container = unityContainer;
        }

        public IoCManager()
        {
            _container = new UnityContainer();
        }

        public void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        public void RegisterType<T>(bool singleton = false)
        {
            if (singleton)
                _container.RegisterType<T>(new ContainerControlledLifetimeManager());
            else
                _container.RegisterType<T>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}