using Dapper;
using DDDTalk.Dominio.Infra.Crosscutting;
using DDDTalk.Dominio.Infra.Crosscutting.Core;
using DDDTalk.Dominio.Turmas;
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
            var sql = "INSERT INTO Turmas (Id, Descricao, LimiteAlunos, TotalInscritos,LimiteIdade) VALUES (@Id, @Descricao, @LimiteAlunos, @TotalInscritos, @LimiteIdade)";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var resultado = conexao.Execute(sql, new { turma.Id, turma.Descricao, turma.LimiteAlunos, turma.TotalInscritos, turma.LimiteIdade });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir a turma");
                return turma;
            }
        }

        public Resultado<Turma, Falha> Recuperar(string id)
        {
            var sql = "SELECT Id, Descricao, LimiteAlunos, TotalInscritos, LimiteIdade FROM Turmas WHERE Id = @id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var query = conexao.Query<dynamic>(sql, new { id }).ToList();
                if (!query.Any())
                    return Falha.Nova(404, "Nenhuma turma encontrada");
                return query.Select(a => new Turma((string)a.Id, (string)a.Descricao, (int)a.LimiteIdade, (int)a.LimiteAlunos, (int)a.TotalInscritos))
                .FirstOrDefault();
            }
        }
    }
}
