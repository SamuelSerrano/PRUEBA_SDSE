using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webapi_clima.Models;
using webapi_clima.Utils;

namespace webapi_clima.Controllers
{
    public class LoginController : ApiController
    {
        private bd_climaEntities db = new bd_climaEntities();

        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult PostLogin(Usuarios usuarios)
        {
           
            // SDSE - 11/05/2021
            // Se encripta el password
            string pwd_hash = Encrypt.GetSHA256(usuarios.clave_usuario);
            usuarios.clave_usuario = pwd_hash;

            Usuarios usuarios_login = (from u in db.Usuarios
                                       where u.nombre_usuario == usuarios.nombre_usuario && u.clave_usuario == usuarios.clave_usuario
                                       select u).FirstOrDefault();

            if (usuarios_login == null)
            {
                return NotFound();
            }

            return Ok(usuarios_login);
        }
    }
}
