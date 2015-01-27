#region Using statemetns

using System.Data.Entity;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using PharmacySolution.BusinessLogic.Managers;
using PharmacySolution.BusinessLogic.Validators;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;
using PharmacySolution.Data;
using PharmacySolution.Data.Repository;

#endregion

namespace PharmacySolution.IOC.CastleWindsor.Installers
{
    public class AdminInstaller : IWindsorInstaller
    {
        private const string WebAssembletyName = "PharmacySolution.Web";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed(WebAssembletyName)
                .BasedOn<IController>()
                .LifestyleTransient()
                .Configure(x => x.Named(x.Implementation.FullName)));

            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<WindsorControllerFactory>());
            // пока что буду делать коменты для себя, вдруг чего то накосячу или забуду
            // Зарегили все что нужно для того что бы IOC Container создал все что мне необходимо 
            container.Register(Component.For<DbContext>().ImplementedBy<DataContext>().LifestyleSingleton());
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)).LifestyleTransient());
            container.Register(Component.For(typeof(IManager<>)).ImplementedBy(typeof(Manager<>)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Medicament>)).ImplementedBy(typeof(MedicamentValidator)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Pharmacy>)).ImplementedBy(typeof(PharmacyValidator)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Order>)).ImplementedBy(typeof(OrderValidator)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<OrderDetails>)).ImplementedBy(typeof(OrderDetailsValidator)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<MedicamentPriceHistory>)).ImplementedBy(typeof(MedicamentPriceHistoryValidator)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Storage>)).ImplementedBy(typeof(StorageValidator)).LifestylePerWebRequest());
            
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}