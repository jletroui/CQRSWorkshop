using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Commands;
using Ncqrs.Config.StructureMap;
using Ncqrs.Eventing.Storage.SQL;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using ReadModel.Denormalizers;
using CommandHandlers;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding;
using ReadModel;

namespace Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Customer", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ExecuteResourceSQLScript("DBCreation", "Website.CQRSDBCreation.sql");
            ExecuteResourceSQLScript("EventStore", "Website.CQRSEventStore.sql");
            ExecuteResourceSQLScript("ReadModel", "Website.CQRSReadModel.sql");

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            var config = new StructureMapConfiguration(cfg =>
            {
                cfg.For<ICommandService>().Use(InitializeCommandService);
                cfg.For<IEventBus>().Use(InitializeEventBus);
                cfg.For<IEventStore>().Use(InitializeEventStore);
            });

            NcqrsEnvironment.Configure(config);

            // Load some initial data
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new RegisterNewCustomerCommand(Guid.NewGuid(), "Matthieu"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new RegisterNewCustomerCommand(Guid.NewGuid(), "Julien"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "The fellowship of the ring"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "The 2 towers"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "The return of the king"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "Spirited Away"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "Ponyo on the cliff by the sea"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "Porco Rosso"));
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new AddNewMediaCommand(Guid.NewGuid(), "Princess Mononoke"));
        }

        private static ICommandService InitializeCommandService()
        {
            var service = new Ncqrs.Commanding.ServiceModel.CommandService();

            service.RegisterExecutor(new AddNewMediaCommandHandler());
            service.RegisterExecutor(new RegisterNewCustomerCommandHandler());
            service.RegisterExecutor(new CustomerRentMediaCommandHandler());
            //service.RegisterExecutor(new CustomerReturnMediaCommandHandler());

            return service;
        }

        private static IEventStore InitializeEventStore()
        {
            return new MsSqlServerEventStore(ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString);
        }

        private static void ExecuteResourceSQLScript(string connStringKey, string cmdResource)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringKey].ConnectionString))
            {
                conn.Open();
                using (var stream = typeof(MvcApplication).Assembly.GetManifestResourceStream(cmdResource))
                using (var reader = new StreamReader(stream))
                using (var cmd = new SqlCommand(reader.ReadToEnd(), conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static IEventBus InitializeEventBus()
        {
            var bus = new InProcessEventBus();
            bus.RegisterAllHandlersInAssembly(typeof(MediaItemDenormalizer).Assembly);

            return bus;
        }
    }
}