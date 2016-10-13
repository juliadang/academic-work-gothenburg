using Newtonsoft.Json;
using SimpleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWebApi.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Register(string name, string password)
        {
            User user = new User { UserName = name, Password = password };

            return Ok(JsonConvert.SerializeObject(user));
        }
    }
}
