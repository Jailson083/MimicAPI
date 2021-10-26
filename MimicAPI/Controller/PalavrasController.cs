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
        private new readonly MimicContext _context;

        public PalavrasController(MimicContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var mimic = await _context.Palavras
                .AsNoTracking()
                .ToListAsync();

            return Ok(_context.Palavras);

        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList(int id)
        {
            var mimic = await _context.Palavras
           .AsNoTracking()
           .Where(p => p.Id.Equals(id))
           .ToListAsync();

            if (mimic == null)
                return BadRequest("Palavra não encontrada!");

            return Ok(mimic);
        }

        [HttpGet("getDetails")]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                var mimicDetails = await _context.Palavras
               .AsNoTracking()
               .Where(p => p.Id.Equals(id))
               .ToListAsync();

                return Ok(_context.Palavras);
            }
            catch (Exception)
            {
                return BadRequest("Palavra não encontrada!");
            }

        }

        [HttpPost("create")]
        public async Task<IActionResult> PostAsync(PalavrasViewModel model)
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
                _context.Palavras.Add(palavras);
                await _context.SaveChangesAsync();

                return Ok(palavras);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(PalavrasViewModel model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var mimicUpdate = await _context.Palavras
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
                _context.Palavras.Update(mimicUpdate);
                await _context.SaveChangesAsync();

                return Ok(mimicUpdate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var mimicDelete = await _context.Palavras
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (mimicDelete is null)
                return BadRequest("Palavra não encontrada");

            try
            {
                _context.Palavras.Remove(mimicDelete);
                await _context.SaveChangesAsync();

                return Ok("Palavra excluída!");

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
