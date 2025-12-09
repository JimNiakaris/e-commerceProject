using e_commerce_API.HelperMethods;
using e_commerce_Core.Entities;
using e_commerce_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_API.Controllers
{
    [ApiController]  // the ApiController decoration can handle model state errors 
    [Route("api/[controller]")]
    public class BaseAPIController :ControllerBase
    {
        protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repo, ISpecification<T> spec, int pageIndex, int pageSize)
            where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var pagination = new PaginationHelper<T>(pageIndex,pageSize,count,items);

            return Ok(pagination);
        }

    }
}
