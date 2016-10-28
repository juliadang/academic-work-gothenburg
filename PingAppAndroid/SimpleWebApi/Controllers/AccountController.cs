using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        public async Task<IHttpActionResult> LoginUser(LoginUserBindingModel loginUserModel)
        {
            var result = await SignInManager.PasswordSignInAsync(loginUserModel.Username, loginUserModel.Password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                 
                    return Ok();
                //break;
                default:
                    return InternalServerError();
                    //break;
            };
        }

        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alreadyRegistered = _applicationDbContext.Users.SingleOrDefault(u => u.UserName == createUserModel.Username);

            if (alreadyRegistered == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = createUserModel.Username,
                    Email = createUserModel.Email
                };

                IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

                if (!addUserResult.Succeeded)
                {
                    return GetErrorResult(addUserResult);
                }

                Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                return Created(locationHeader, TheModelFactory.Create(user));
            }
            else
            {
                return Ok("You are already registered");
            }
        }

        [Authorize]
        //[HttpPost]
        [Route("add/{username2}")]
        public IHttpActionResult AddFriend(string username2)
        {
            string username1 = User.Identity.Name;
            if (username1.ToLower() == username2.ToLower())
            {
                return Ok("We know you love yourself but you cannot add yourself as a friend");
            }
            var alreadyFriend = _applicationDbContext.Users.SingleOrDefault(f => f.UserName == username2);

            if (alreadyFriend == null)
            {
                return Ok("The user does not exist");
            }
            else
            {
                var friendList = _applicationDbContext.Friendships.Where(friendship => (friendship.Username1 == username1 && friendship.Username2 == username2)
                                                                                    || (friendship.Username2 == username1 && friendship.Username1 == username2)).ToList();

                if (friendList.Count <= 0)
                {
                    Friendships newFriend = new Friendships(username1, username2);
                    this._applicationDbContext.Friendships.Add(newFriend);
                    _applicationDbContext.SaveChanges();
                    return Ok("Friend added");
                }
                else
                {
                    return Ok("You are already friends");
                }
            }
        }

        [Authorize]
        [Route("getfriendlist")]
        public IHttpActionResult GetFriendlist()
        {
            var username1 = User.Identity.Name;
            var friendList = _applicationDbContext.Friendships.Where(friendship => (friendship.Username1 == username1) || (friendship.Username2 == username1)).Select(n => n.Username1 == username1 ? n.Username2 : n.Username1).ToList();

            return Ok(friendList);
        }

        //[Authorize]
        [Route("sendping/{receiver}")]
        public IHttpActionResult SendPing(string receiver)
        {
            //MessageSender notification = new MessageSender("PING!", User.Identity.Name, receiver);
            MessageSender notification = new MessageSender("PING!", "Oliver", receiver);
            notification.SendMessage();

            return Ok("Ping sent");
        }
    }
}
