using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClima.Models
{
	public class ClimaCiudad
	{
	    public int id { get; set; }
		public int idCiudad { get; set; }
		public int idClima { get; set; }
		public int tempMax { get; set; }
		public int tempMin { get; set; }
		public DateTime fecha { get; set; }

		public virtual Ciudades Ciudades { get; set; }
		public virtual Climas Climas { get; set; }
	}
}