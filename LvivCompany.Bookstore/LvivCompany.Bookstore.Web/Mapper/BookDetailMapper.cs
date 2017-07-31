﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.ViewModels;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public class BookDetailMapper:IMapper<Book, BookInfo>
    {
        private IMapper mapper;

        public BookDetailMapper()
        {

            var MapperForAuthor = new AuthorMapper();

            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Book, BookDetailViewModel>()
                .ForMember(bv => bv.Authors, b => b.MapFrom(
               a => MapperForAuthor.Map(a.BookAuthors.Select(c => c.Author)).ToList()))
                .ForMember(bv => bv.Category, b => b.MapFrom(
                a => a.Category.Name))
                .ForMember(bv => bv.Publisher, b => b.MapFrom(
                a => a.Publisher.Name));

            });
            
            mapper = config.CreateMapper();
        }

        public BookInfo Map(Book entity) => mapper.Map<Book, BookInfo>(entity);
        public Book Map(BookInfo entity) => mapper.Map<BookInfo, Book>(entity);
    }
}
