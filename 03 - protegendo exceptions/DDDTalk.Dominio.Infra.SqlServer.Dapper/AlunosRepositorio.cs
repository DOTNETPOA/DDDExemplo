using Dapper;
using DDDTalk.Dominio.Alunos;
using DDDTalk.Dominio.Infra.Crosscutting;
using DDDTalk.Dominio.Infra.Crosscutting.Core;
using DDDTalk.Dominio.Turmas;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DDDTalk.Dominio.Infra.SqlServer.Dapper
{
    public sealed class AlunosRepositorio : IAlunosRepositorio
    {
        private readonly AppSettingsHelper _AppSettingsHelper;

        public AlunosRepositorio(AppSettingsHelper _appSettingsHelper)
        {
            _AppSettingsHelper = _appSettingsHelper;
        }

        public Resultado<Aluno, Falha> IncluirESalvar(Aluno aluno)
        {
            var sucesso = false;
            var sqlAluno = "INSERT INTO Alunos (Id, Nome, Email, DataNascimento) VALUES (@Id, @Nome, @Email, @DataNascimento)";
            var sqlInscricao = "INSERT INTO Inscricoes (Id, AlunoId, TurmaId, InscritoEm) VALUES (@Id, @AlunoId, @TurmaId, @InscritoEm)";
            var updateTurma = "UPDATE Turmas SET TotalInscritos = @TotalInscritos WHERE Id = @Id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var resultado = conexao.Execute(sqlAluno, new { aluno.Id, aluno.Nome, aluno.Email, aluno.DataNascimento }, transacao);
                        if (resultado <= 0)
                            return Falha.Nova(400, "Não foi possível incluir o aluno");
                        foreach (var inscricao in aluno.Inscricoes)
                        {
                            resultado = conexao.Execute(sqlInscricao, new {inscricao.Id, AlunoId = aluno.Id, TurmaId = inscricao.Turma.Id, inscricao.InscritoEm }, transacao);
                            if (resultado <= 0)
                                return Falha.Nova(400, "Não foi possível incluir inscrição para o aluno");
                            resultado = conexao.Execute(updateTurma, new { inscricao.Turma.Id, inscricao.Turma.TotalInscritos }, transacao);
                            if (resultado <= 0)
                                return Falha.Nova(400, "Não foi possível atualizar a turma");
                        }
                        transacao.Commit();
                        sucesso = true;
                        return aluno;
                    }
                    catch (Exception ex)
                    {
                        return Falha.NovaComException(ex);
                    }
                    finally
                    {
                        if (!sucesso)
                            transacao.Rollback();
                        conexao.Close();
                    }
                }
            }
        }

        public Resultado<Aluno, Falha> Atualizar(Aluno aluno)
        {
            var sucesso = false;
            var sqlAluno = "UPDATE Alunos SET Nome = @Nome, Email = @Email, DataNascimento = @DataNascimento WHERE Id = @Id";
            var sqlInscricaoInsert = "INSERT INTO Inscricoes (Id, AlunoId, TurmaId, InscritoEm) VALUES (@Id, @AlunoId, @TurmaId, @InscritoEm)";
            var sqlInscricaoDelete = "DELETE FROM Inscricoes WHERE Id = @Id";
            var updateTurma = "UPDATE Turmas SET TotalInscritos = @TotalInscritos WHERE Id = @Id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var alunoPreAlteracao = Recuperar(aluno.Id);
                        if (alunoPreAlteracao.EhFalha)
                            return alunoPreAlteracao.Falha;
                        var resultado = conexao.Execute(sqlAluno, new { aluno.Id, aluno.Nome, aluno.Email, aluno.DataNascimento }, transacao);
                        if (resultado <= 0)
                            return Falha.Nova(400, "Não foi possível atualizar o aluno");
                        foreach (var inscricao in aluno.Inscricoes)
                        {
                            if(!alunoPreAlteracao.Sucesso.Inscricoes.Any(a=> a.Id == inscricao.Id))
                            {
                                resultado = conexao.Execute(sqlInscricaoInsert, new { inscricao.Id, AlunoId = aluno.Id, TurmaId = inscricao.Turma.Id, inscricao.InscritoEm }, transacao);
                                if (resultado <= 0)
                                    return Falha.Nova(400, "Não foi possível incluir inscrição");
                                resultado = conexao.Execute(updateTurma, new { inscricao.Turma.Id, inscricao.Turma.TotalInscritos }, transacao);
                                if (resultado <= 0)
                                    return Falha.Nova(400, "Não foi possível atualizar a turma");
                            }
                        }

                        foreach (var inscricao in alunoPreAlteracao.Sucesso.Inscricoes)
                        {
                            if (!aluno.Inscricoes.Any(a => a.Id == inscricao.Id))
                            {
                                resultado = conexao.Execute(sqlInscricaoDelete, new { inscricao.Id }, transacao );
                                if (resultado <= 0)
                                    return Falha.Nova(400, "Não foi possível excluir inscrição");
                                resultado = conexao.Execute(updateTurma, new { inscricao.Turma.Id, inscricao.Turma.TotalInscritos }, transacao);
                                if (resultado <= 0)
                                    return Falha.Nova(400, "Não foi possível atualizar a turma");
                            }
                        }
                        transacao.Commit();
                        sucesso = true;
                        return aluno;
                    }
                    catch (Exception ex)
                    {
                        return Falha.NovaComException(ex);
                    }
                    finally
                    {
                        if (!sucesso)
                            transacao.Rollback();
                        conexao.Close();
                    }
                }
            }
        }

        public Resultado<Aluno, Falha> RecuperarPorEmail(string email)
        {
            var sqlAluno = @"SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Email = @email;";
            var sqlInscricao = "SELECT Id, AlunoId, TurmaId, InscritoEm FROM Inscricoes WHERE AlunoId IN (SELECT Id FROM Alunos WHERE Email = @email)";
            var sqlTurmas = "SELECT Id, Descricao, LimiteAlunos, TotalInscritos, LimiteIdade FROM Turmas WHERE Id IN (SELECT TurmaId FROM Inscricoes WHERE AlunoId = (SELECT Id FROM Alunos WHERE Email = @email))";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var alunoQuery = conexao.Query<dynamic>(sqlAluno, new { email }).ToList();
                if (!alunoQuery.Any())
                    return Falha.Nova(404, "Nenhum aluno encontrado");
                var turmas = conexao
                                .Query<dynamic>(sqlTurmas, new { email })
                                .Select(t=> new Turma((string)t.Id, (string)t.Descricao, (int)t.LimiteIdade, (int)t.LimiteAlunos, (int)t.TotalInscritos))
                                .ToList();
                var inscricoes = conexao
                                    .Query<dynamic>(sqlInscricao, new { email })
                                    .Select(i => new Aluno.Inscricao((string)i.Id, (string)i.AlunoId, turmas.FirstOrDefault(t=> t.Id.Equals((string)i.TurmaId)), (DateTime)i.InscritoEm))
                                    .ToList();
                return alunoQuery.Select(a => new Aluno((string)a.Id, (string)a.Nome, (string)a.Email, (DateTime)a.DataNascimento, inscricoes))
                .FirstOrDefault();
            }
        }

        public Resultado<Aluno, Falha> Recuperar(string id)
        {
            var sqlAluno = @"SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Id = @id;";
            var sqlInscricao = "SELECT Id, TurmaId, InscritoEm FROM Inscricoes WHERE AlunoId IN (SELECT Id FROM Alunos WHERE Id = @id)";
            var sqlTurmas = "SELECT Id, Descricao, LimiteAlunos, TotalInscritos, LimiteIdade FROM Turmas WHERE Id IN (SELECT TurmaId FROM Inscricoes WHERE AlunoId = (SELECT Id FROM Alunos WHERE Id = @id))";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var alunoQuery = conexao.Query<dynamic>(sqlAluno, new { id }).ToList();
                if (!alunoQuery.Any())
                    return Falha.Nova(404, "Nenhum aluno encontrado");
                var turmas = conexao
                                .Query<dynamic>(sqlTurmas, new { id })
                                .Select(t => new Turma((string)t.Id, (string)t.Descricao, (int)t.LimiteIdade, (int)t.LimiteAlunos, (int)t.TotalInscritos))
                                .ToList();
                var inscricoes = conexao
                                    .Query<dynamic>(sqlInscricao, new { id })
                                    .Select(i => new Aluno.Inscricao((string)i.Id, (string)i.AlunoId, turmas.FirstOrDefault(t => t.Id.Equals((string)i.TurmaId)), (DateTime)i.InscritoEm))
                                    .ToList();
                return alunoQuery.Select(a => new Aluno((string)a.Id, (string)a.Nome, (string)a.Email, (DateTime)a.DataNascimento, inscricoes))
                .FirstOrDefault();
            }
        }
    }
}
