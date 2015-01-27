using Microsoft.Practices.Unity;
using PharmacySolution.BusinessLogic.Managers;
using PharmacySolution.Contracts.Manager;
using PharmacySolution.Contracts.Repository;
using PharmacySolution.Contracts.Validator;
using PharmacySolution.Core;
using PharmacySolution.IOC;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repository = IOCContainer.GetInstance().GetUnityContainer().Resolve<IRepository<Pharmacy>>();
            var validator = IOCContainer.GetInstance().GetUnityContainer().Resolve<IValidator<Pharmacy>>();
            var manager = IOCContainer.GetInstance().GetUnityContainer().Resolve<IManager<Pharmacy>>();
        }
    }
}
