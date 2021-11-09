﻿using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Queries;

public class FilterProductCategoriesQuery : IRequest<Response<FilterProductCategoryDto>>
{
    public FilterProductCategoriesQuery(FilterProductCategoryDto filter)
    {
        Filter = filter;
    }

    public FilterProductCategoryDto Filter { get; set; }
}