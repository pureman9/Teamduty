using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace MyApplication.Models
{
    public class MemberList
    {
      
      //  public Member memberViewModel { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RegisterCode { get; set; }
        public string ConfirmRegisterCode { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public int Credit { get; set; }

    
    }
}