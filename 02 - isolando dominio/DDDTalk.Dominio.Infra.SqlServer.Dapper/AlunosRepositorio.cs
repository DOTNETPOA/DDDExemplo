using Dapper;
using DDDTalk.Dominio.Infra.Crosscutting;
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

        public Aluno IncluirESalvar(Aluno aluno)
        {
            var sql = "INSERT INTO Alunos (Id, Nome, Email, DataNascimento) VALUES (@Id, @Nome, @Email, @DataNascimento)";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var resultado = conexao.Execute(sql, new { aluno.Id, aluno.Nome, aluno.Email, aluno.DataNascimento });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir o aluno");
                return aluno;
            }
        }

        public Aluno RecuperarPorEmail(string email)
        {
            var sql = "SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Email = @email";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var alunoQuery = conexao.Query<dynamic>(sql, new { email }).ToList();
                if (!alunoQuery.Any())
                    return null;
                return alunoQuery.Select(a => new Aluno((string)a.Id, (string)a.Nome, (string)a.Email, (DateTime)a.DataNascimento))
                .FirstOrDefault();
            }
        }

        public Aluno Recuperar(string id)
        {
            var sql = "SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Id = @id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var alunoQuery = conexao.Query<dynamic>(sql, new { id }).ToList();
                if (!alunoQuery.Any())
                    return null;
                return alunoQuery.Select(a => new Aluno((string)a.Id, (string)a.Nome, (string)a.Email, (DateTime)a.DataNascimento))
                .FirstOrDefault();
            }
        }
    }
}
