using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PingAppServer.Models
{
    public class DatabaseUtils
    {
        PingdbContext _context;
        public DatabaseUtils(PingdbContext context)
        {
            _context = context;
        }

        internal void RegisterUser(PingUsers user)
        {
            _context.PingUsers.Add(new PingUsers
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            });
            _context.SaveChanges();
        }

        internal List<PingUsers> GetUsers()
        {
            List<PingUsers> users = _context.PingUsers.Select(u => u).ToList();
            return users;
        }
    }
}