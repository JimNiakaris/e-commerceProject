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

            return query;
        }

    }
}
