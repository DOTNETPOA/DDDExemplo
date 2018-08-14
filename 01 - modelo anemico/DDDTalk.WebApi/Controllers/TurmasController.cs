using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDTalk.WebApi.Infra;
using DDDTalk.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDTalk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly TurmasRepositorio _turmasRepositorio;

        public TurmasController(TurmasRepositorio turmasRepositorio)
        {
            _turmasRepositorio = turmasRepositorio;
        }

        [HttpPost]
        public IActionResult Nova([FromBody]Turma novaTurma)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                _turmasRepositorio.Nova(novaTurma);

                return CreatedAtAction(nameof(Recuperar), new { novaTurma.Id }, novaTurma);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpGet]
        public IActionResult Recuperar(string id)
        {
            try
            {
                var turma = _turmasRepositorio.Recuperar(id);
                if (turma == null)
                    return NotFound("Nenhuma turma com o id desejado");
                return Ok(turma);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}