using e_commerce_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        //we use the lambda expression inside the base() because the base constructor
        //expects to see a type of Expression<Func<T, bool>>? called criteria.
        // REF: BaseSpecification.cs:11
        //the criteria is going to be used by the Evaluator and apply it to the DbContext of the EF Core.
        //So the product specification is going to return the expression into the var spec in the ProductsController
        //REF: ProductsController.cs:34

        public ProductSpecification(string? brand,string? type, string? sort) 
            :base(x=>
            (string.IsNullOrEmpty(brand) || x.Brand==brand) &&
            (string.IsNullOrEmpty(type)) || x.Type==type)
        {
            switch(sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(p=>p.Price); 
                    break;
                default:
                    AddOrderBy(n=>n.Name); 
                    break;
            }
         }
    }
}
