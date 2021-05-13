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
using WebClima.Filters;
using WebClima.Models;
using WebClima.Models.ViewModels;

namespace WebClima.Controllers
{
    public class AdminController : Controller
    {
        private string URI = WebConfigurationManager.AppSettings["UriAPI"];
        // GET: Admin
        [AutorizaRol(iRol: 2)]
        public async Task<ActionResult> Index()
        {
            List<ClimaAdViewModel> oclimaCiudad = null;           
            try
            {
               
                string Uri = URI+"ClimaCiudades";                
                var httpClient = new HttpClient();

                var httpResponse = await httpClient.GetAsync(Uri);
                if (httpResponse.Content != null)
                {
                    string responseContent = await httpResponse.Content.ReadAsStringAsync();
                    if (responseContent.Equals("[]")) ViewData["Error"] = "No Hay datos";
                    else
					{
                        oclimaCiudad = (List<ClimaAdViewModel>)JsonConvert.DeserializeObject(responseContent, typeof(List<ClimaAdViewModel>));
                        foreach(ClimaAdViewModel CAV in oclimaCiudad) 
                        {
                            CAV.NombreCiudad = await obtenerValorCiudad(CAV.IdCiudad);
                            CAV.NombreClima = await obtenerValorClima(CAV.IdClima);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Error." + ex.Message;
            }
            return View(oclimaCiudad);
            

        }

        public async Task<string> obtenerValorCiudad(int id)
		{
            var httpClient = new HttpClient();
            Ciudades oCiudad = null;
            string result = "";
            var httpResponse = await httpClient.GetAsync(URI+"Ciudades/"+id.ToString());
            if (httpResponse.Content != null)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                oCiudad =  (Ciudades)JsonConvert.DeserializeObject(responseContent, typeof(Ciudades));
                result = oCiudad.nombre;
            }
            return result;
            }

        public async Task<string> obtenerValorClima(int id)
        {
            var httpClient = new HttpClient();
            Climas oClima = null;
            string result = "";
            var httpResponse = await httpClient.GetAsync(URI+"Climas/" + id.ToString());
            if (httpResponse.Content != null)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                oClima = (Climas)JsonConvert.DeserializeObject(responseContent, typeof(Climas));
                result = oClima.clima;
            }
            return result;
        }

        [AutorizaRol(iRol: 2)]
        public async Task<ActionResult> Insert() {

            string Uri = URI+"Climas";
            var httpClient = new HttpClient();
            List<Climas> oclima = null;
            var httpResponse = await httpClient.GetAsync(Uri);
            if (httpResponse.Content != null)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                if (responseContent.Equals("[]")) ViewData["Error"] = "No Hay datos";
                else
                {
                    oclima = (List<Climas>)JsonConvert.DeserializeObject(responseContent, typeof(List<Climas>));
                    List<SelectListItem> lst = new List<SelectListItem>();
                    foreach (Climas c in oclima)
					{
                        lst.Add(new SelectListItem() { Text = c.clima, Value = c.id.ToString() });
                    }
                    ViewBag.Clima = lst;
                }
            }

             Uri = URI+"Ciudades";
             httpClient = new HttpClient();
            List<Ciudades> oCiudad = null;
            httpResponse = await httpClient.GetAsync(Uri);
            if (httpResponse.Content != null)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                if (responseContent.Equals("[]")) ViewData["Error"] = "No Hay datos";
                else
                {
                    oCiudad = (List<Ciudades>)JsonConvert.DeserializeObject(responseContent, typeof(List<Ciudades>));
                    List<SelectListItem> lstciudad = new List<SelectListItem>();
                    foreach (Ciudades c in oCiudad)
                    {
                        lstciudad.Add(new SelectListItem() { Text = c.nombre, Value = c.id.ToString() });
                    }
                    ViewBag.Ciudad = lstciudad;
                }
            }


            return View();
        }

        [AutorizaRol(iRol: 2)]
        [HttpPost]
        public async Task<ActionResult> Insert(ClimaAdViewModel model)
        {
			try 
            { 
             if(ModelState.IsValid)
				{
                    ClimaCiudad oclimaciudad = new ClimaCiudad
                    {
                        idCiudad = model.IdCiudad,
                        idClima = model.IdClima,
                        tempMax = model.TempMax,
                        tempMin = model.TempMin,
                        fecha = model.Fecha
                    };
                    string Uri = URI+"ClimaCiudades";
                    var contentjson = JsonConvert.SerializeObject(oclimaciudad);
                    HttpContent c = new StringContent(contentjson, Encoding.UTF8, "application/json");
                    var httpClient = new HttpClient();
                    var httpResponse = await httpClient.PostAsync(Uri, c);

                    return Redirect("/Admin");
                }

                return RedirectToAction("Insert", "Admin"); ;
            }
            catch(Exception e)
			{

			}
            return View();
        }


    }
}