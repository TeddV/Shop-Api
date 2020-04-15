
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;

namespace CategoryController.Controllers
{
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return new List<Category>();
        }    

        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<Category>> GetById(long id)
        {
            return new Category();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post([FromBody]Category model)
        {
            if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            return Ok(model);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<ActionResult<List<Category>>> Put(long id, [FromBody]Category model)
        {
            //Verifica se o Id informado é o mesmo do model
            if(id != model.Id)
            {
                return NotFound(new { message = "Categoria não encontrada" });
            }

            //Verifica se is dados são validos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(model);     
        }
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult<List<Category>>> Delete()
        {
            return Ok();
        }
    }
}