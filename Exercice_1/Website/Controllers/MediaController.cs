using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadModel;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Commands;


namespace Website.Controllers
{
    [HandleError]
    public class MediaController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ReadModelContainer())
            {
                var query = from media in context.MediaItems select media;
                return View(query.ToArray());
            }
        }

        [HttpPost]
        public RedirectToRouteResult Add(string title)
        {
            NcqrsEnvironment.Get<ICommandService>().Execute(new CreateMedia(Guid.NewGuid(), title));

            return RedirectToAction("Index");
        }
    }
}
