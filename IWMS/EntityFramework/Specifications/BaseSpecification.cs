using EntityFramework.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFramework.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Filter { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        protected BaseSpecification(Expression<Func<T, bool>> filter)
        {
            Filter = filter;
        }

        protected BaseSpecification()
        {
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }


    }
}
