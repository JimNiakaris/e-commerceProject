using e_commerce_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_Core.Specifications
{
    public class TypeListSpecification : BaseSpecification<Product,string>
    {
        public TypeListSpecification()
        {
            AddSelect(t=>t.Type);
            ApplyDistict();
        }
    }
}
