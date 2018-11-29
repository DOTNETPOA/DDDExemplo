using System;
using System.Linq;
using DDDTalk.Dominio;
using DDDTalk.Dominio.Infra.Crosscutting.Core;
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
                if ( Aluno.Novo(novoAluno.Nome, novoAluno.Email, novoAluno.DataNascimento) is var aluno && aluno.EhFalha)
                    return StatusCode(aluno.Falha.Codigo, aluno.Falha);
                if (_alunosRepositorio.RecuperarPorEmail(aluno.Sucesso.Email).EhSucesso)
                    return BadRequest(Falha.Nova(400, "Email já está em uso: " + novoAluno.Email));
                if (_alunosRepositorio.IncluirESalvar(aluno.Sucesso) is var resultado && resultado.EhFalha)
                    return StatusCode(resultado.Falha.Codigo, resultado.Falha);

                return CreatedAtAction(nameof(Recuperar), new { aluno.Sucesso.Id },
                    new AlunoViewModel(aluno.Sucesso.Id, aluno.Sucesso.Nome, aluno.Sucesso.Email, aluno.Sucesso.Idade(DateTime.Now),
                                        aluno.Sucesso.Inscricoes.Select(a => new InscricaoViewModel(aluno.Sucesso.Id, new InscricaoViewModel.TurmaViewModel(a.Turma.Id, a.Turma.Descricao), a.InscritoEm)).ToList()));
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
                if( _alunosRepositorio.Recuperar(id) is var aluno && aluno.EhFalha)
                    return StatusCode(aluno.Falha.Codigo, aluno.Falha);
                return Ok(new AlunoViewModel(aluno.Sucesso.Id, aluno.Sucesso.Nome, aluno.Sucesso.Email, aluno.Sucesso.Idade(DateTime.Now),
                                aluno.Sucesso.Inscricoes.Select(a => new InscricaoViewModel(aluno.Sucesso.Id, new InscricaoViewModel.TurmaViewModel(a.Turma.Id, a.Turma.Descricao), a.InscritoEm)).ToList()));
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
                if (_alunosRepositorio.Recuperar(alunoId) is var aluno && aluno.EhFalha)
                    return StatusCode(aluno.Falha.Codigo, aluno.Falha);

                var turma = _turmasRepositorio.Recuperar(novaInscricao.TurmaId);
                if (turma == null)
                    return BadRequest("Turma informada é inválida");

                if (aluno.Sucesso.RealizarInscricao(turma) is var inscricao && inscricao.EhFalha)
                    return StatusCode(inscricao.Falha.Codigo, inscricao.Falha);

                if (_alunosRepositorio.Atualizar(aluno.Sucesso) is var resultado && resultado.EhFalha)
                    return StatusCode(resultado.Falha.Codigo, resultado.Falha);

                return CreatedAtAction(nameof(RecuperarInscricao), new { alunoId, inscricao.Sucesso.Id }, 
                        new InscricaoViewModel(aluno.Sucesso.Id, new InscricaoViewModel.TurmaViewModel(inscricao.Sucesso.Turma.Id, inscricao.Sucesso.Turma.Descricao), inscricao.Sucesso.InscritoEm));
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
                if(_alunosRepositorio.Recuperar(alunoId) is var aluno && aluno.EhFalha)
                    return StatusCode(aluno.Falha.Codigo, aluno.Falha);
                var inscricao = aluno.Sucesso.Inscricoes.FirstOrDefault(i => i.Id.Equals(id));
                if (inscricao == null)
                    return NotFound("Nenhuma inscrição referente ao id desejado");

                return Ok(new InscricaoViewModel(aluno.Sucesso.Id, new InscricaoViewModel.TurmaViewModel(inscricao.Turma.Id, inscricao.Turma.Descricao), inscricao.InscritoEm));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

    }
}
