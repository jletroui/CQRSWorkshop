using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadModel;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Commands;
using Website.Models;

namespace Website.Controllers
{
    [HandleError]
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ReadModelContainer())
            {
                var query = from Customer in context.CustomerItems select Customer;
                return View(query.ToArray());
            }
        }

        [HttpPost]
        public RedirectToRouteResult Register(string name)
        {
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new RegisterNewCustomerCommand(Guid.NewGuid(), name));

            return RedirectToAction("Index");
        }

        public ViewResult Rent(Guid customerId)
        {
            using (var context = new ReadModelContainer())
            {
                var customer = (from c in context.CustomerItems where c.Id == customerId select c).Single();
                var rentedMedia = (from rented in context.CustomerRentedItems select rented.MediaId).ToArray();
                var query = from m in context.MediaItems where !rentedMedia.Contains(m.Id) select m;

                return View(new CustomerMoviesModel()
                    {
                        CustomerId = customer.Id,
                        CustomerName = customer.Name,
                        Medias = query.ToArray()
                    });
            }
        }

        [HttpPost]
        public RedirectToRouteResult DoRent(Guid customerId, Guid mediaId)
        {
            // In real life, should catch BusinessRuleViolatedException in order to display nice message to user
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new CustomerRentMediaCommand(customerId, mediaId));

            return RedirectToAction("Index");
        }

        public ViewResult Return(Guid customerId)
        {
            using (var context = new ReadModelContainer())
            {
                var query = (from rented in context.CustomerRentedItems where rented.CustomerId == customerId select rented).ToArray();

                return View(new CustomerMoviesModel()
                {
                    CustomerId = query[0].CustomerId,
                    CustomerName = query[0].CustomerName,
                    RentedMedias = query
                });
            }
        }

        [HttpPost]
        public RedirectToRouteResult DoReturn(Guid customerId, Guid mediaId)
        {
            // In real life, should catch BusinessRuleViolatedException in order to display nice message to user
            NcqrsEnvironment.Get<ICommandService>().Execute(
                new CustomerReturnMediaCommand(customerId, mediaId));

            return RedirectToAction("Index");
        }

    }
}
