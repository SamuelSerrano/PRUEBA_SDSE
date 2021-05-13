using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebClima.Models;

namespace WebClima.Controllers
{
    
    public class AccesoController : Controller
    {
        private string URI = WebConfigurationManager.AppSettings["UriAPI"];
        public ActionResult Login()
		{
            return View();
		}

        [HttpPost]
        public async Task<ActionResult> Login(string user, string pass)
        {
             Usuario usuario = new Usuario
              {
                  Nombre_usuario = user,
                  Clave_usuario = pass
             };

			try
			{
                Usuario model = null;
                string Uri = URI+"login";
                var contentjson = JsonConvert.SerializeObject(usuario);
                HttpContent c = new StringContent(contentjson, Encoding.UTF8, "application/json");
                var httpClient = new HttpClient();
                
                var httpResponse = await httpClient.PostAsync(Uri, c);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Usuario>(responseContent);
                    if (model == null) ViewData["Error"] = "Datos Invalidos";
					else 
                    {
                        Session["User"] = model;                        
                    }
                }
                
            }
            catch(Exception ex) 
            {
                ViewData["Error"] = "Error."+ex.Message;
            }
              

              return RedirectToAction("Index","Home");
            
        }
    }
}