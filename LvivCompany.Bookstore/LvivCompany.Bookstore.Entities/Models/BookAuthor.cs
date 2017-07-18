using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class BookAuthor
    {
       
        public long BookId { get; set; }
        public Book Book { get; set; }


        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
