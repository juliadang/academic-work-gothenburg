using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        //PingdbContext _dbContext;
        //DatabaseUtils _dm;
        //public AccountController(PingdbContext context)
        //{
        //    _dbContext = context;
        //    _dm = new DatabaseUtils(context);
        //    _dbContext.Database.ensurecreated();
        //}

        UserManager<IdentityUser> _accountManager;
      

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


        //public IHttpActionResult EditProfile()
        //{

        //}
        //public IHttpActionResult GetFriend()
        //{

        //}
        
        //public IHttpActionResult AddFriend()
        //{

        //}
        //public IHttpActionResult RemoveFriend()

    }
}
