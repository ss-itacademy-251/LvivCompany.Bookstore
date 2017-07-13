﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LvivCompany.Bookstore.Entities
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}