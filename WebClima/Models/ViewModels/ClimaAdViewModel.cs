using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClima.Models.ViewModels
{
	public class ClimaAdViewModel
	{
		[Required]
		[Display(Name = "Clima")]
		public int IdClima { get; set; }

		public string NombreClima { get; set; }
		public string NombreCiudad { get; set; }
		[Required]
		[Display(Name="Ciudad")]
		public int IdCiudad { get; set; }
		public int Id { get; set; }
		[Required]
		[Display(Name = "Temperatura Max")]
		[Range(-20, 100, ErrorMessage = "Ingrese una temperatura entre -20 | 100 grados centigrados")]
		public int TempMax { get; set; }
		[Required]
		[Display(Name = "Temperatura Min")]
		[Range(-20, 100, ErrorMessage = "Ingrese una temperatura entre -20 | 100 grados centigrados")]
		public int TempMin { get; set; }
		[Display(Name = "Fecha")]
		[DataType(DataType.Date)]
		public DateTime Fecha { get; set; }
	}
}