using System;
using DDDTalk.Dominio;
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
                if (Turma.Nova(novaTurma.Descricao, novaTurma.LimiteAlunos) is var turma && turma.EhFalha)
                    return StatusCode(turma.Falha.Codigo, turma.Falha);
                _turmasRepositorio.AdicionarESalvar(turma.Sucesso);

                return CreatedAtAction(nameof(Recuperar), new { turma.Sucesso.Id }, new TurmaViewModel(turma.Sucesso.Id, turma.Sucesso.Descricao, turma.Sucesso.VagasDisponiveis  ));
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
                if (_turmasRepositorio.Recuperar(id) is var turma && turma.EhFalha)
                    return StatusCode(turma.Falha.Codigo, turma.Falha);
                return Ok(new TurmaViewModel(turma.Sucesso.Id, turma.Sucesso.Descricao, turma.Sucesso.VagasDisponiveis));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}