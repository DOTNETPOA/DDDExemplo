using System;
using System.Linq;
using DDDTalk.Dominio;
using DDDTalk.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDDTalk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;

        public AlunosController(
            IAlunosRepositorio alunosRepositorio,
            ITurmasRepositorio turmasRepositorio)
        {
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
        }

        [HttpPost]
        public IActionResult Criar([FromBody]AlunoInputModel novoAluno)
        {
            try
            {
                var aluno = Aluno.Novo(novoAluno.Nome, novoAluno.Email, novoAluno.DataNascimento);
                if (_alunosRepositorio.RecuperarPorEmail(aluno.Email) != null)
                    return BadRequest("Email já está em uso: " + novoAluno.Email);
                _alunosRepositorio.IncluirESalvar(aluno);

                return CreatedAtAction(nameof(Recuperar), new { aluno.Id }, new AlunoViewModel(aluno.Id, aluno.Nome, aluno.Email, aluno.Idade(DateTime.Now)));
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
                var aluno = _alunosRepositorio.Recuperar(id);
                if (aluno == null)
                    return NotFound("Nenhum aluno referente ao id desejado");
                return Ok(new AlunoViewModel(aluno.Id, aluno.Nome, aluno.Email, aluno.Idade(DateTime.Now)));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost("{alunoId}/Inscricoes")]
        public IActionResult RealizarInscricao(string alunoId, [FromBody]InscricaoInputModel novaInscricao)
        {
            try
            {                
                var aluno = _alunosRepositorio.Recuperar(alunoId);
                if (aluno == null)
                    return NotFound("Aluno inválido");

                var turma = _turmasRepositorio.Recuperar(novaInscricao.TurmaId);
                if (turma == null)
                    return BadRequest("Turma informada é inválida");

                var inscricao = aluno.RealizarInscricao(turma);

                return CreatedAtAction(nameof(RecuperarInscricao), new { alunoId, inscricao.Id }, new InscricaoViewModel(aluno.Id, turma.Id, inscricao.InscritoEm));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpGet("{alunoId}/Inscricoes/{id}")]
        public IActionResult RecuperarInscricao(string alunoId, string id)
        {
            try
            {
                var aluno = _alunosRepositorio.Recuperar(alunoId);
                if (aluno == null)
                    return NotFound("Nenhum aluno referente ao id desejado");
                var inscricao = aluno.Inscricoes.FirstOrDefault(i => i.Id.Equals(id));
                if (inscricao == null)
                    return NotFound("Nenhuma inscrição referente ao id desejado");

                return Ok(new InscricaoViewModel(aluno.Id, inscricao.Turma.Id, inscricao.InscritoEm));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

    }
}
