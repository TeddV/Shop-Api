
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace CategoryController.Controllers
{
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get(
            [FromServices] DataContext context
        )
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
                return Ok(categories);
        }    

        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<Category>> GetById(
            long id,
            [FromServices] DataContext context
        )   
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return category;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post(   
            [FromBody]Category model,
            [FromServices] DataContext context
        )
        {
            if(!ModelState.IsValid)   
                return BadRequest(ModelState);
                
            try
            {    

            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possivel criar a categoria"}); 
            }
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<ActionResult<List<Category>>> Put(
            long id,
            [FromBody]Category model,
            [FromServices] DataContext context
        )   
        {
            //Verifica se o Id informado é o mesmo do model
            if(id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            //Verifica se is dados são validos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model); 
            }

            catch(DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });
            }     

            catch
            {
                return BadRequest(new { message = "Não foi possivel atualizar a categoria." });

            }     
                
        }
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult<List<Category>>> Delete(
            long id,
            [FromServices]DataContext context
        )
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound(new { message = "Categoria não encontrada"});

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = " Não foi possivel remover a categoria"});
            }
        }
    }
}