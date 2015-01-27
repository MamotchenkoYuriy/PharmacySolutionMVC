using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using PharmacySolution.BusinessLogic.Managers;
using PharmacySolution.BusinessLogic.Validators;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;
using PharmacySolution.Data;
using PharmacySolution.Data.Repository;

namespace PharmacySolution.IOC
{
    public class IOCContainer
    {
        private static IOCContainer _container;
        private static IUnityContainer _unityContainer;

        private IOCContainer()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<DataContext>(new ContainerControlledLifetimeManager()); // Не здыхаем в общем =))
            _unityContainer.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            _unityContainer.RegisterType<IValidator<MedicamentPriceHistory>, MedicamentPriceHistoryValidator>();
            _unityContainer.RegisterType<IValidator<Medicament>, MedicamentValidator>();
            _unityContainer.RegisterType<IValidator<Pharmacy>, PharmacyValidator>();
            _unityContainer.RegisterType<IValidator<Order>, OrderValidator>();
            _unityContainer.RegisterType<IValidator<OrderDetails>, OrderDetailsValidator>();
            _unityContainer.RegisterType<IValidator<Storage>, StorageValidator>();
            _unityContainer.RegisterType(typeof(IManager<>), typeof(Manager<>));


            //_unityContainer
            //    .RegisterType(typeof(IRepository<>), typeof(Repository<>))
            //    .RegisterType<DataContext>(new ContainerControlledLifetimeManager())
            //    .RegisterType(typeof(IValidator<MedicamentPriceHistory>), typeof(MedicamentPriceHistoryValidator))
            //    .RegisterType(typeof(IValidator<Medicament>), typeof(MedicamentValidator))
            //    .RegisterType(typeof(IValidator<OrderDetails>), typeof(OrderDetailsValidator))
            //    .RegisterType(typeof(IValidator<Core.Pharmacy>), typeof(PharmacyValidator))
            //    .RegisterType(typeof(IValidator<Storage>), typeof(StorageValidator));
        }

        public IUnityContainer GetUnityContainer()
        {
            return _unityContainer;
        }

        public static IOCContainer GetInstance()
        {
            if (_container != null)
            {
                return _container;
            }
            _container = new IOCContainer();
            return _container;
        }
    }
}
