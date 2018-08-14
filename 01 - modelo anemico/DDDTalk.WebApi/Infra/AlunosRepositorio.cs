using Dapper;
using DDDTalk.WebApi.Helpers;
using DDDTalk.WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DDDTalk.WebApi.Infra
{
    public sealed class AlunosRepositorio
    {
        private readonly AppSettingsHelper _AppSettingsHelper;

        public AlunosRepositorio(AppSettingsHelper _appSettingsHelper)
        {
            _AppSettingsHelper = _appSettingsHelper;
        }

        public void Novo(Aluno aluno)
        {
            var sql = "INSERT INTO Alunos (Id, Nome, Email, DataNascimento) VALUES (@Id, @Nome, @Email, @DataNascimento)";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                aluno.Id = Guid.NewGuid().ToString();
                var resultado = conexao.Execute(sql, new { aluno.Id, aluno.Nome, aluno.Email, aluno.DataNascimento });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir o aluno");
            }
        }

        public Aluno RecuperarPorEmail(string email)
        {
            var sql = "SELEC Id, Nome, Email, DataNascimento FROM Alunos WHERE Email = @email";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var alunoQuery = conexao.Query<dynamic>(sql, new { email }).ToList();
                if (!alunoQuery.Any())
                    return null;
                return alunoQuery.Select(a => new Aluno
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    DataNascimento = a.DataNascimento
                })
                .FirstOrDefault();
            }
        }

        public Aluno Recuperar(string id)
        {
            var sql = "SELEC Id, Nome, Email, DataNascimento FROM Alunos WHERE Id = @id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var alunoQuery = conexao.Query<dynamic>(sql, new { id }).ToList();
                if (!alunoQuery.Any())
                    return null;
                return alunoQuery.Select(a => new Aluno
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    DataNascimento = a.DataNascimento
                })
                .FirstOrDefault();
            }
        }
    }
}
