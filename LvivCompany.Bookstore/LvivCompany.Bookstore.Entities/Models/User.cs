using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace LvivCompany.Bookstore.Entities
{
    public class User: IdentityUser<long>
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Photo { get; set; }

    }
}
