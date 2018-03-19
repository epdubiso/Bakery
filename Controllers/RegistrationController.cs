using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery2018.Models;

namespace Bakery2018.Controllers
{
    public class RegistrationController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Registration
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonLastName, PersonFirstName,"+
             "PersonEmail, PersonPhone" )]Person p)
        {


            db.People.Add(p);
            db.SaveChanges();



            return RedirectToAction("Index");




        }
    }
}