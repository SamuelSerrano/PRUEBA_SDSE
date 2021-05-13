using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClima.Models;

namespace WebClima.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
	public class AutorizaRol : AuthorizeAttribute
	{
		private Usuario oUser;
		private int Rol;
		public AutorizaRol(int iRol=0)
		{
			this.Rol = iRol;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			try
			{

				oUser = (Usuario)HttpContext.Current.Session["User"];
				if(oUser == null)
				{
					filterContext.Result = new RedirectResult("~/Acceso/Login/");
				}else
				 if(oUser.IdRol != this.Rol) 
				{
					 
					filterContext.Result = new RedirectResult("~/Home/");
				}
			}
			catch(Exception e)
			{
				filterContext.Result = new RedirectResult("~/Home/");
			}
			//base.OnAuthorization(filterContext);
		}
	}
}