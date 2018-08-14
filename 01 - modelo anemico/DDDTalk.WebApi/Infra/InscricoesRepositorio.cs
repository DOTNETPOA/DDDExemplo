using Dapper;
using DDDTalk.WebApi.Helpers;
using DDDTalk.WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DDDTalk.WebApi.Infra
{
    public sealed class InscricoesRepositorio
    {
        private readonly AppSettingsHelper _AppSettingsHelper;

        public InscricoesRepositorio(AppSettingsHelper _appSettingsHelper)
        {
            _AppSettingsHelper = _appSettingsHelper;
        }

        public void Nova(Inscricao novaInscricao)
        {
            var sql = "INSERT INTO Inscricoes (Id, AlunoId, TurmaId, InscritoEm) VALUES (@Id, @AlunoId, @TurmaId, @InscritoEm)";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                novaInscricao.Id = Guid.NewGuid().ToString();
                var resultado = conexao.Execute(sql, new { novaInscricao.Id, novaInscricao.AlunoId, novaInscricao.TurmaId, novaInscricao.InscritoEm });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir a inscrição");
            }
        }

        public Inscricao Recuperar(string id)
        {
            var sql = "SELEC Id, AlunoId, TurmaId, InscritoEm FROM Inscricoes WHERE Id = @id";
            using (var conexao = new SqlConnection(_AppSettingsHelper.GetConnectionString()))
            {
                var query = conexao.Query<dynamic>(sql, new { id }).ToList();
                if (!query.Any())
                    return null;
                return query.Select(a => new Inscricao
                {
                    Id = a.Id,
                    AlunoId = a.AlunoId,
                    TurmaId = a.TurmaId,
                    InscritoEm = a.InscritoEm
                })
                .FirstOrDefault();
            }
        }
    }
}
