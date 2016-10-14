using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleWebApi.Models
{
    public class PingUser : IdentityUser
    {
        public string UserName { get; set; }
    }
}