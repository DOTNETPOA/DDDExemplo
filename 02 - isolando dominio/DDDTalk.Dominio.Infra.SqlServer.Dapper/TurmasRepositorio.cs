using Dapper;
using DDDTalk.Dominio.Infra.Crosscutting;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DDDTalk.Dominio.Infra.SqlServer.Dapper
{
    public sealed class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly AppSettingsHelper _AppSettingsHelper;

        public TurmasRepositorio(AppSettingsHelper _appSettingsHelper)
        {
            _AppSettingsHelper = _appSettingsHelper;
        }

        public Turma AdicionarESalvar(Turma turma)
        {
            var sql = "INSERT INTO Turmas (Id, Descricao, LimiteAlunos) VALUES (@Id, @Descricao, @LimiteAlunos)";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var resultado = conexao.Execute(sql, new { turma.Id, turma.Descricao, turma.LimiteAlunos });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir a turma");
                return turma;
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
                return query.Select(a => new Turma((string)a.Id, (string)a.Descricao, (int)a.LimiteAlunos, (int)a.TotalInscritos))
                .FirstOrDefault();
            }
        }
    }
}
