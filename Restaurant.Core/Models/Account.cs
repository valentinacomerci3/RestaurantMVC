using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Models
{
   
        public enum Role
        {
            Manager,
            Client
        }
        public class Account
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public Role Role { get; set; }
        }
    
}
