﻿using System.Linq;
using Zora.Shared.Application;

namespace Zora.Shared.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> Sort<TEntity>(
            this IQueryable<TEntity> queryable, 
            SortOrder<TEntity> sortOrder) 
            => sortOrder.Order == SortOrder<TEntity>.Descending 
                ? queryable.OrderByDescending(sortOrder.ToExpression()) 
                : queryable.OrderBy(sortOrder.ToExpression());
    }
}
