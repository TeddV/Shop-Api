using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
       
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Post(   
            [FromServices] DataContext context,
            [FromBody]Product model
        )
        {
            if(ModelState.IsValid)
            {         
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            } 
            else
            {
                return BadRequest(new { message = "Não foi possivel criar o produto"}); 

            }                               
            
        }
    }
    
}