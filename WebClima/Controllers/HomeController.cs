using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClima.Filters;

namespace WebClima.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{			
			return View();
		}

		public ActionResult Logout()
		{
			Session["User"]=null;
			return RedirectToAction("Login", "Acceso"); ;
		}

		[AutorizaRol(iRol:1)]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		[AutorizaRol(iRol: 2)]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}