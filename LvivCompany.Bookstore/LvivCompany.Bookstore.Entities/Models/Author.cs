using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LvivCompany.Bookstore.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
     

    }
}
