using System;
using System.Collections.Generic;
using System.Text;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
