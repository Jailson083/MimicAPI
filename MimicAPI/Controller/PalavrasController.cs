using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Database;
using MimicAPI.Models;
using MimicAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalavrasController : ControllerBase
    {
        
        [HttpGet("getAll")]    
        public async Task<IActionResult> GetAll([FromServices]MimicContext context)
        {
            var mimic = await context.Palavras
                .AsNoTracking()
                .ToListAsync();

            return Ok(mimic);

        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList([FromServices]MimicContext context, int id)
        {
            try
            {
                var mimic = await context.Palavras
               .AsNoTracking()
               .Where(p => p.Id.Equals(id))
               .ToListAsync();

                return Ok(mimic);
            }
            catch (Exception)
            {
                return BadRequest("Palavra não encontrada!");
            }
           
        }

        [HttpGet("getDetails")]
        public async Task<IActionResult> GetDetails([FromServices]MimicContext context, int id)
        {
            try
            {
                var mimicDetails = await context.Palavras
               .AsNoTracking()
               .Where(p => p.Id.Equals(id))
               .ToListAsync();

                return Ok(mimicDetails);
            }
            catch (Exception)
            {
                return BadRequest("Palavra não encontrada!");
            }
           
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostAsync([FromServices]MimicContext context, [FromBody] PalavrasViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var palavras = new Palavra
            {
                Name = model.Name,
                Active = model.Active,
                CreatedOn = DateTime.Now,
                Punctuation = model.Punctuation
                
            };

            try
            {
                context.Palavras.Add(palavras);
                await context.SaveChangesAsync();

                return Ok(palavras);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromServices]MimicContext context, [FromBody] PalavrasViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var mimicUpdate = await context.Palavras
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (mimicUpdate is null)
                return BadRequest("Palavra não encontrada!");

            mimicUpdate.Name = model.Name;
            mimicUpdate.Active = model.Active;
            mimicUpdate.Punctuation = model.Punctuation;
            mimicUpdate.ModifiedOn = DateTime.Now;

            try
            {
                context.Palavras.Update(mimicUpdate);
                await context.SaveChangesAsync();

                return Ok(mimicUpdate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices]MimicContext context, [FromRoute] int id)
        {
            var mimicDelete = await context.Palavras
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (mimicDelete is null)
                return BadRequest("Palavra não encontrada");

            try
            {
                context.Palavras.Remove(mimicDelete);
                await context.SaveChangesAsync();

                return Ok("Palavra excluída!");

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
