using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class AuthorMapper : IMapper<Author, AuthorFullName>
    {
        private IMapper mapper;

        public AuthorMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Author, AuthorFullName>()
                    .ForMember(dest => dest.FullName,
                    opts => opts.MapFrom(
                       src => string.Format("{0} {1}",
                           src.FirstName,
                           src.LastName)));

            });
            mapper = config.CreateMapper();
        }

        public AuthorFullName Map(Author entity) => mapper.Map<Author, AuthorFullName>(entity);
        public List<AuthorFullName> Map(IEnumerable<Author> entity) => mapper.Map<IEnumerable<Author>, List<AuthorFullName>>(entity);
        public Author Map(AuthorFullName entity) => mapper.Map<AuthorFullName, Author>(entity);
    }
}
