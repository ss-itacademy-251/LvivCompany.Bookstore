using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class BookMapper : IMapper<Book, BookInfo>
    {
        private IMapper mapper;

        public BookMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookInfo>();
            });
            mapper = config.CreateMapper(); 
        }

        public BookInfo Map(Book entity) => mapper.Map<Book, BookInfo>(entity);
        public Book Map(BookInfo entity) => mapper.Map<BookInfo, Book>(entity);
        public List<BookInfo> Map(IEnumerable<Book> entity) => mapper.Map<IEnumerable<Book>, List<BookInfo>>(entity);
        public IEnumerable<Book> Map(List<BookInfo> entity) => mapper.Map<List<BookInfo>, IEnumerable<Book>>(entity);
    }
}
