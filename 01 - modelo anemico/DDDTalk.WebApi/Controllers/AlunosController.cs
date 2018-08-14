﻿using System;
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
        private readonly TurmasRepositorio _turmasRepositorio;
        private readonly InscricoesRepositorio _inscricoesRepositorio;

        public AlunosController(
            AlunosRepositorio alunosRepositorio,
            TurmasRepositorio turmasRepositorio,
            InscricoesRepositorio inscricoesRepositorio)
        {
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _inscricoesRepositorio = inscricoesRepositorio;
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

        [HttpPost("{alunoId}/Inscricoes")]
        public IActionResult RealizarInscricao(string alunoId, [FromBody]Inscricao novaInscricao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var aluno = _alunosRepositorio.Recuperar(alunoId);
                if (aluno == null)
                    return NotFound("Aluno inválido");

                var turma = _turmasRepositorio.Recuperar(novaInscricao.TurmaId);
                if (turma == null)
                    return BadRequest("Turma informada é inválida");

                if (turma.TotalInscritos + 1 > turma.LimiteAlunos)
                    return BadRequest("Não existe mais vagas para a turma");

                novaInscricao.AlunoId = alunoId;
                novaInscricao.InscritoEm = DateTime.Now;
                _inscricoesRepositorio.Nova(novaInscricao);

                return CreatedAtAction(nameof(RecuperarInscricao), new { alunoId, novaInscricao.Id }, novaInscricao);
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
                var inscricao = _inscricoesRepositorio.Recuperar(id);
                if (inscricao == null)
                    return NotFound("Nenhuma inscrição referente ao id desejado");
                return Ok(inscricao);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

    }
}
