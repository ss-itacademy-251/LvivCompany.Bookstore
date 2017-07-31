using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public static class Mappers
    {
        public static MapperConfiguration MapperForBook()
        {
         
            
            IMapper mapper = MapperForAuthor().CreateMapper();
            
            var config= new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookDetailViewModel>()
                .ForMember(bv => bv.Authors, b => b.MapFrom(
                a => mapper.Map<IEnumerable<Author>, List<AuthorFullName>>(a.BookAuthors.Select(c => c.Author)).ToList()))
                .ForMember(bv => bv.Category, b => b.MapFrom(
                a => a.Category.Name))
                .ForMember(bv => bv.Publisher, b => b.MapFrom(
                a => a.Publisher.Name));

        });
            return config;
        }

        public static MapperConfiguration MapperForAuthor()
        {
            var config = new MapperConfiguration(cfg => {
            cfg.CreateMap < Author, AuthorFullName>()
                .ForMember(dest => dest.FullName,
                opts => opts.MapFrom(
                   src => string.Format("{0} {1}",
                       src.FirstName,
                       src.LastName)));

        });
            return config;
        }

        public static MapperConfiguration MapperForBookInfo()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookInfo>();
            });
            return config;
        }
    }
}
