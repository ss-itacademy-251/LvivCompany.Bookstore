﻿using System.Collections.Generic;

namespace LvivCompany.Bookstore.BusinessLogic.Mapper
{
    public interface IMapper<Tsource, TDestination>
    {
        Tsource Map(TDestination entity);

        TDestination Map(Tsource entity);

        Tsource Map(TDestination model, Tsource entity);

        List<TDestination> Map(List<Tsource> entity);

        List<Tsource> Map(List<TDestination> entity);
    }
}
