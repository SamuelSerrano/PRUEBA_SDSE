using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebClima.Filters;
using WebClima.Models;
using WebClima.Models.ViewModels;

namespace WebClima.Controllers
{
    public class ConsultaController : Controller
    {
        private string URI = WebConfigurationManager.AppSettings["UriAPI"];
        // GET: Consulta
        [AutorizaRol(iRol: 1)]
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
                        foreach (ClimaAdViewModel CAV in oclimaCiudad)
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
            var httpResponse = await httpClient.GetAsync(URI+"Ciudades/" + id.ToString());
            if (httpResponse.Content != null)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                oCiudad = (Ciudades)JsonConvert.DeserializeObject(responseContent, typeof(Ciudades));
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
    }
}