using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Models
{
    public class DatabaseUtils
    {
        PingdbContext _context;
        public DatabaseUtils(PingdbContext context)
        {
            _context = context;
        }

        //internal void RegisterUser(PingUser user)
        //{
        //    _context.PingUsers.Add(new PingUsers
        //    {
        //        Username = user.Username,
        //        Email = user.Email,
        //        Password = user.Password
        //    });
        //    _context.SaveChanges();
        //}

        //internal List<PingUser> GetUsers()
        //{
        //    List<PingUser> users = _context.PingUsers.Select(u => u).ToList();
        //    return users;
        //}
    }
}