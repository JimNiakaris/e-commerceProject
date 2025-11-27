using e_commerce_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        //private readonly Expression<Func<T, bool>> _criteria = criteria;

        //we will use again, the primary constructor
        //public BaseSpecification(Expression<Func<T, bool>> criteria)
        //{
        //    _criteria = criteria;
        //}

        //Empty Constructor
        protected BaseSpecification() : this(null)
        {

        }

        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy {get; private set;}

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

    }
}
