using e_commerce_Core.Entities;
using e_commerce_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); 
                //it's like doing this
                //var query = context.Products.AsQueryable();
                //if (!string.IsNullOrWhiteSpace(brand))
                //{
                //    query = query.Where(b => b.Brand == brand);
                //}
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc!= null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if (spec.IsDistinct)
            {
                query = query.Distinct();
            }

            if(spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, ISpecification<T,TResult> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
                //it's like doing this
                //var query = context.Products.AsQueryable();
                //if (!string.IsNullOrWhiteSpace(brand))
                //{
                //    query = query.Where(b => b.Brand == brand);
                //}
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            var selectQuery = query as IQueryable<TResult>;

            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }
            if (spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }

            if (spec.IsPagingEnabled)
            {
                selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
            }

            return selectQuery ?? query .Cast<TResult>();
        }
    }
}
