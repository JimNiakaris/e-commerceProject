using e_commerce_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_Core.Specifications
{
    public class BrandListSpecification :BaseSpecification<Product,string>
    {
        public BrandListSpecification()
        {
            AddSelect(b=>b.Brand);
            ApplyDistict();
        }
    }
}
