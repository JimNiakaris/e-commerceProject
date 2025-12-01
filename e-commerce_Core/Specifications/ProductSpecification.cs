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
        public ProductSpecification(string? brand,string? type, string? sort) :base(x=>
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
