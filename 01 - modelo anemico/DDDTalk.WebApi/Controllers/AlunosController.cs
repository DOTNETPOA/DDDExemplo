using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDTalk.WebApi.Infra;
using DDDTalk.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDDTalk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AlunosRepositorio _alunosRepositorio;

        public AlunosController(AlunosRepositorio alunosRepositorio)
        {
            _alunosRepositorio = alunosRepositorio;
        }

        [HttpPost]
        public IActionResult Criar([FromBody]Aluno novoAluno)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_alunosRepositorio.RecuperarPorEmail(novoAluno.Email) != null)
                    return BadRequest("Email já está em uso: " + novoAluno.Email);

                _alunosRepositorio.Novo(novoAluno);

                return CreatedAtAction(nameof(Recuperar), new { novoAluno.Id }, novoAluno);
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
                var aluno = _alunosRepositorio.Recuperar(id);
                if (aluno == null)
                    return NotFound("Nenhum aluno referente ao id desejado");
                return Ok(aluno);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        //[HttpPost("{alunoId}/Inscricoes")]
        //public IActionResult RealizarInscricao(string alunoId, [FromBody]Inscricao novaInscrucao)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        if (_inscricoesRepositorio.RecuperarPorAluno(novoAluno.Email) != null)
        //            return BadRequest("Email já está em uso: " + novoAluno.Email);

        //        _alunosRepositorio.Novo(novoAluno);

        //        return CreatedAtAction(nameof(Recuperar), new { novoAluno.Id }, novoAluno);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, new { error = e.Message });
        //    }
        //}
    }
}
