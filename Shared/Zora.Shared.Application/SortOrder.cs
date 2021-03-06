﻿using System;
using System.Linq.Expressions;

namespace Zora.Shared.Application
{
     public abstract class SortOrder<TEntity>
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";

        protected SortOrder(string? sortBy, string? order)
        {
            this.SortBy = sortBy;
            this.Order = order;
        }

        public string? SortBy { get; }

        public string? Order { get; }

        public abstract Expression<Func<TEntity, object>> ToExpression();
    }
}
