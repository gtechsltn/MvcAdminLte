using MvcAdminLte.Entities;
using MvcAdminLte.Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Mvc;

namespace MvcAdminLte.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult Test()
        {
            var model = new TestModel
            {
                Id = 1,
                BoD = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Test(TestModel model)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine($"Id: {model.Id}, BoD: {model.BoD}");

                Thread.Sleep(5000);

                var dt = model.BoD;
                var entity = new Test
                {
                    Id = model.Id,
                    BoD = dt
                };

                Debug.WriteLine($"Id: {entity.Id}, BoD: {entity.BoD}");

                var blnRet = false;
                if (model.Id % 2 == 0)
                {
                    blnRet = true;
                }
                else
                {
                    blnRet = false;
                }

                return Json(blnRet, JsonRequestBehavior.AllowGet);
            }
            return View(model);
        }
    }
}