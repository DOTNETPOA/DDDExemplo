using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DDDTalk.Dominio.Infra.SqlServer.Dapper")]
namespace DDDTalk.Dominio
{
    public sealed class Inscricao
    {
        internal Inscricao(string id, Turma turma, DateTime inscritoEm)
        {
            Id = id;
            Turma = turma;
            InscritoEm = inscritoEm;
        }

        public string Id { get; }
        public Turma Turma { get; }
        public DateTime InscritoEm { get; }

        public static Inscricao Nova(Turma turma)
        {
            if (turma.VagasDisponiveis <= 0)
                throw new InvalidOperationException("Sem vagas disponiveis");
            return new Inscricao(Guid.NewGuid().ToString(), turma, DateTime.Now);
        }
    }
}
