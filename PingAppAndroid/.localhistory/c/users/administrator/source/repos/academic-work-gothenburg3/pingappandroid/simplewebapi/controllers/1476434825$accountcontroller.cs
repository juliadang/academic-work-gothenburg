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
        DatabaseUtils _dm = new DatabaseUtils(new PingdbContext());

        [HttpGet]
        public IHttpActionResult Register(string name, string password, string email)
        {
            PingUsers user = new PingUsers { Username = name, Password = password, Email = email };

            _dm.RegisterUser(user);
            return Ok(JsonConvert.SerializeObject(user));
        }
        
        public IHttpActionResult Login()
        {
            List<PingUsers> users =_dm.GetUsers();

            return Ok(JsonConvert.SerializeObject(users));
        }

        //public IHttpActionResult AddFriend()
        //{

        //}

        //public IHttpActionResult EditProfile()
        //{

        //}
        //public IHttpActionResult GetFriend()
        //{

        //}
        
        //public IHttpActionResult RemoveFriend()

    }
}
