using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_Core.Specifications
{
    //we are going to use an object for the parameters passed on the controller instead of using, brand, type and sort
    //because as the application grows more and more parameters will be needed and is messy to just passing more parameters to the controller

    public class ProductSpecificationParameters
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value>MaxPageSize) ? MaxPageSize : value;
        }



        private List<string> _brands = [];
        public List<string> Brands
        {
            get => _brands;
            set
            {                               //we do that because the api route is going to send brands=React,Angular for example
                                            //so we need to split them to the comma, and also remove emtpy entries like, brands=React, 
                _brands = value.SelectMany(b=>b.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        private List<string> _types = [];

        public List<string> Types
        {
            get => _types;
            set
            {                               //we do that because the api route is going to send brands=React,Angular for example
                                            //so we need to split them to the comma, and also remove emtpy entries like, brands=React, 
                _types = value.SelectMany(b => b.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        public string? Sort { get; set; }

        private string? _search;
        public string? Search
        {
            get => _search ?? "";
            set => _search = value;
        }
    }
}
