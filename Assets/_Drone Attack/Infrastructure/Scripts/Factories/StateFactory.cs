using Shot_Shift.Infrastructure.Scripts;
using Zenject;

namespace _Drone_Attack.Infrastructure.Scripts.Factories
{
    public class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) =>
            _container = container;

        public T CreateState<T>() where T : IExitableState =>
            _container.Resolve<T>();
    }
}
