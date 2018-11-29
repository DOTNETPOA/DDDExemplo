using DDDTalk.Dominio.Alunos;
using DDDTalk.Dominio.Comandos;
using DDDTalk.Dominio.Infra.Crosscutting.Core;
using DDDTalk.Dominio.Turmas;
using System;
using System.Linq;

namespace DDDTalk.Dominio.Aplicacao
{
    public interface INovoAlunoCommandHandler
    {
        Resultado<Aluno, Falha> Executar(NovoAlunoComando comando);
    }

    public interface IRealizarInscricaoCommandHandler
    {
        Resultado<Aluno.Inscricao, Falha> Executar(RealizarInscricaoComando comando);
    }

    public sealed class GerenciadorComandosAluno : INovoAlunoCommandHandler, IRealizarInscricaoCommandHandler
    {
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;

        public GerenciadorComandosAluno(
            IAlunosRepositorio alunosRepositorio,
            ITurmasRepositorio turmasRepositorio)
        {
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
        }

        public Resultado<Aluno, Falha> Executar(NovoAlunoComando comando)
        {
            try
            {
                if (Aluno.Novo(comando.Nome, comando.Email, comando.DataNascimento) is var aluno && aluno.EhFalha)
                    return aluno.Falha;
                if (_alunosRepositorio.RecuperarPorEmail(aluno.Sucesso.Email).EhSucesso)
                    return Falha.Nova(400, "Email já está em uso: " + comando.Email);
                if (_alunosRepositorio.IncluirESalvar(aluno.Sucesso) is var resultado && resultado.EhFalha)
                    return  resultado.Falha;
                return resultado.Sucesso;
            }
            catch (Exception e)
            {
                return Falha.Nova(500, "Erro ao incluir novo aluno");
            }
        }

        public Resultado<Aluno.Inscricao, Falha> Executar(RealizarInscricaoComando comando)
        {
            try
            {
                if (_alunosRepositorio.Recuperar(comando.AlunoId) is var aluno && aluno.EhFalha)
                    return aluno.Falha;

                if (_turmasRepositorio.Recuperar(comando.TurmaId) is var turma && turma.EhFalha)
                    return turma.Falha;

                if (aluno.Sucesso.RealizarInscricao(turma.Sucesso) is var inscricao && inscricao.EhFalha)
                    return inscricao.Falha;

                if (_alunosRepositorio.Atualizar(aluno.Sucesso) is var resultado && resultado.EhFalha)
                    return resultado.Falha;
                return resultado.Sucesso.Inscricoes.FirstOrDefault(i=>i.Id.Equals(inscricao.Sucesso.Id));
            }
            catch (Exception e)
            {
                return Falha.Nova(500, "Erro ao realizar inscrição");
            }
        }
    }
}
