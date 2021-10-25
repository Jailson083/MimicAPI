using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controller
{
    [ApiController]
    [Route("api/controller")]
    public class PalavrasController : ControllerBase
    {
        
        [HttpGet]
        [Route("getAll")]

        public async Task<IActionResult> Get([FromServices]MimicContext context)
        {
            var mimic = await context.Palavras
                .AsNoTracking()
                .ToListAsync();

            return Ok(mimic);

        }

    }
}
