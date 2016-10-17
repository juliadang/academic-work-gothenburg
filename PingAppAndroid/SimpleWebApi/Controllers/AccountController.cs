using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SimpleWebApi.Infrastructure;
using SimpleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimpleWebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
       [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Route("login")]
        public async Task<IHttpActionResult> LoginUser(string userName, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email
                //FirstName = createUserModel.FirstName,
                //LastName = createUserModel.LastName,
                //Level = 3,
                //JoinDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }

        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email
                //FirstName = createUserModel.FirstName,
                //LastName = createUserModel.LastName,
                //Level = 3,
                //JoinDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }
        #region Bortkommenterad kod
        //PingdbContext _dbContext;
        //DatabaseUtils _dm;
        //public AccountController(PingdbContext context)
        //{
        //    _dbContext = context;
        //    _dm = new DatabaseUtils(context);
        //    _dbContext.Database.ensurecreated();
        //}

        //DatabaseUtils _dm = new DatabaseUtils(new PingdbContext());

        //[HttpGet]
        //public IHttpActionResult Register(string name, string password, string email)
        //{
        //    PingUsers user = new PingUsers { Username = name, Password = password, Email = email };

        //    _dm.RegisterUser(user);
        //    return Ok(JsonConvert.SerializeObject(user));
        //}

        //public IHttpActionResult Login()
        //{
        //    List<PingUsers> users =_dm.GetUsers();

        //    return Ok(JsonConvert.SerializeObject(users));
        //}


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
        #endregion

    }
}
