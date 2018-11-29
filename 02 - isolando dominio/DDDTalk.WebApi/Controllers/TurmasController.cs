using System;
using DDDTalk.Dominio;
using DDDTalk.Dominio.Turmas;
using DDDTalk.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDDTalk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly ITurmasRepositorio _turmasRepositorio;

        public TurmasController(ITurmasRepositorio turmasRepositorio)
        {
            _turmasRepositorio = turmasRepositorio;
        }

        [HttpPost]
        public IActionResult Nova([FromBody]TurmaInputModel novaTurma)
        {
            try
            {
                var turma = Turma.Nova(novaTurma.Descricao, novaTurma.LimiteIdade, novaTurma.LimiteAlunos);
                _turmasRepositorio.AdicionarESalvar(turma);

                return CreatedAtAction(nameof(Recuperar), new { turma.Id }, new TurmaViewModel(turma.Id, turma.Descricao, novaTurma.LimiteIdade, turma.VagasDisponiveis  ));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Recuperar(string id)
        {
            try
            {
                var turma = _turmasRepositorio.Recuperar(id);
                if (turma == null)
                    return NotFound("Nenhuma turma com o id desejado");
                return Ok(new TurmaViewModel(turma.Id, turma.Descricao, turma.LimiteIdade, turma.VagasDisponiveis));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}