using Dapper;
using DDDTalk.Dominio.Infra.Crosscutting;
using DDDTalk.WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DDDTalk.WebApi.Infra
{
    public sealed class TurmasRepositorio
    {
        private readonly AppSettingsHelper _AppSettingsHelper;

        public TurmasRepositorio(AppSettingsHelper _appSettingsHelper)
        {
            _AppSettingsHelper = _appSettingsHelper;
        }

        public void Nova(Turma novaTurma)
        {
            var sql = "INSERT INTO Turmas (Id, Descricao, LimiteAlunos) VALUES (@Id, @Descricao, @LimiteAlunos)";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                novaTurma.Id = Guid.NewGuid().ToString();
                var resultado = conexao.Execute(sql, new { novaTurma.Id, novaTurma.Descricao, novaTurma.LimiteAlunos });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir a turma");
            }
        }

        public Turma Recuperar(string id)
        {
            var sql = "SELECT Id, Descricao, LimiteAlunos, (SELECT COUNT(Id) FROM Inscricoes WHERE TurmaId = @id) AS TotalInscritos FROM Turmas WHERE Id = @id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var query = conexao.Query<dynamic>(sql, new { id }).ToList();
                if (!query.Any())
                    return null;
                return query.Select(a => new Turma
                {
                    Id = a.Id,
                    Descricao = a.Descricao,
                    LimiteAlunos = a.LimiteAlunos,
                    TotalInscritos = a.TotalInscritos
                })
                .FirstOrDefault();
            }
        }
    }
}
