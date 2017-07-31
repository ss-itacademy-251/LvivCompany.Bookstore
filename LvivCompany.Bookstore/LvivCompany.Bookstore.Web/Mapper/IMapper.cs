﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvivCompany.Bookstore.Web.Mapper
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource entity);
        TSource Map(TDestination entity);
    }

}