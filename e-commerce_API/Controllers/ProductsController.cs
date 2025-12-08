using e_commerce_API.HelperMethods;
using e_commerce_Core.Entities;
using e_commerce_Core.Interfaces;
using e_commerce_Core.Specifications;
using e_commerce_Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace e_commerce_API.Controllers
{

    public class ProductsController(IGenericRepository<Product> repo) : BaseAPIController 
        //no longer using the Product Repository but the Generic Repository with Product Entity
    {

        //this is commented out because we are going to use the primary constructor 
        //like we did in the ProductReposiroty
        //Now we are going to inject the IRepository in the Controller using primary constructor
        //private readonly StoreContext _context;

        ////dependency injection
        //public ProductsController(StoreContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]                                               //now we can pass the object in the controller
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecificationParameters specParameters)
        {
            //creating the expression we need to pass to the GENERIC repository
            //this is done in the ProductSpcification deriving from BaseSpecification
            //The ProductSpecification gets the Filters, brand,type
            var spec = new ProductSpecification(specParameters);
            //the spec is used in the GenericRepository that gets the type off Products
            //the ListAsync is defined to use an Expression as a parameter, named spec
            #region this is done in the base controller now
            //var products = await repo.ListAsync(spec);
            //var count = await repo.CountAsync(spec);

            //var pagination = new PaginationHelper<Product>(specParameters.PageIndex, specParameters.PageSize, count, products);
            

            //return Ok(products);
            //return Ok(pagination);
            #endregion

            return await CreatePageResult<Product>(repo, spec,specParameters.PageIndex,specParameters.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if(product == null) return NotFound();
            return product; 
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct (Product product)
        {
            repo.Add(product);

            if (await repo.SaveAllAsync())
            {
                //CreatedAtAction gets as input an ActionResult in this case the GetProduct action of the controller
                //we specify the id which is the id of the newly created product and the return object
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Error on Creating the new Product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if(product.Id != id || !ProductExtists(id))
            {
                return BadRequest("Can not update this product");
            }

            repo.Update(product);

            if(await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Error on updating the Product");
        } 
        private bool ProductExtists(int id)
        {
            return repo.Exists(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if(product == null) return NotFound();

            repo.Remove(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Error on deleting the Product");

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
        }
    }
}
