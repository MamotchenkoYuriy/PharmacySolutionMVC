using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using PharmacySolution.IOC.CastleWindsor.Installers;
using PharmacySolution.Web.Core.Validators;
using FluentValidation.Mvc;

namespace PharmacySolution.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new PharmacyViewValidator();
            var container = new WindsorContainer();
            container.Install(new AdminInstaller());
            var autoMapper = new AutoMapperConfigurator();
            autoMapper.Configure();
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
