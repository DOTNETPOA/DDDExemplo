using DDDTalk.Dominio;
using DDDTalk.Dominio.Aplicacao;
using DDDTalk.Dominio.Comandos;
using DDDTalk.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DDDTalk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly INovoAlunoCommandHandler _novoAlunoHandler;
        private readonly IRealizarInscricaoCommandHandler _realizarInscricaoHandler;
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;

        public AlunosController(
            INovoAlunoCommandHandler novoAlunoHandler,
            IRealizarInscricaoCommandHandler realizarInscricaoHandler,
            IAlunosRepositorio alunosRepositorio,
            ITurmasRepositorio turmasRepositorio)
        {
            _novoAlunoHandler = novoAlunoHandler;
            _realizarInscricaoHandler = realizarInscricaoHandler;
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
        }

        [HttpPost]
        public IActionResult Criar([FromBody]AlunoInputModel novoAluno)
        {
            var resultado = _novoAlunoHandler.Executar(new NovoAlunoComando(novoAluno.Nome, novoAluno.Email, novoAluno.DataNascimento));
            if (resultado.EhFalha)
                return StatusCode(resultado.Falha.Codigo, resultado.Falha);

            return CreatedAtAction(nameof(Recuperar), new { resultado.Sucesso.Id },
                new AlunoViewModel(resultado.Sucesso.Id, resultado.Sucesso.Nome, resultado.Sucesso.Email, resultado.Sucesso.Idade(DateTime.Now),
                                    resultado.Sucesso.Inscricoes.Select(a => new InscricaoViewModel(resultado.Sucesso.Id, new InscricaoViewModel.TurmaViewModel(a.Turma.Id, a.Turma.Descricao), a.InscritoEm)).ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult Recuperar(string id)
        {
            try
            {
                if (_alunosRepositorio.Recuperar(id) is var aluno && aluno.EhFalha)
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
            var resultado = _realizarInscricaoHandler.Executar(new RealizarInscricaoComando(alunoId, novaInscricao.TurmaId));
            if (resultado.EhFalha)
                return StatusCode(resultado.Falha.Codigo, resultado.Falha);
            return CreatedAtAction(nameof(RecuperarInscricao), new { alunoId, resultado.Sucesso.Id },
                    new InscricaoViewModel(resultado.Sucesso.Id, new InscricaoViewModel.TurmaViewModel(resultado.Sucesso.Turma.Id, resultado.Sucesso.Turma.Descricao), resultado.Sucesso.InscritoEm));
        }

        [HttpGet("{alunoId}/Inscricoes/{id}")]
        public IActionResult RecuperarInscricao(string alunoId, string id)
        {
            try
            {
                if (_alunosRepositorio.Recuperar(alunoId) is var aluno && aluno.EhFalha)
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
