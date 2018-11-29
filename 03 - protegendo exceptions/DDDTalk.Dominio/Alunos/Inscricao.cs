using DDDTalk.Dominio.Infra.Crosscutting.Core;
using DDDTalk.Dominio.Turmas;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DDDTalk.Dominio.Infra.SqlServer.Dapper")]
namespace DDDTalk.Dominio.Alunos
{
    public sealed partial class Aluno
    {
        public sealed class Inscricao
        {
            internal Inscricao(string id, string alunoId, Turma turma, DateTime inscritoEm)
            {
                Id = id;
                Turma = turma;
                InscritoEm = inscritoEm;
            }

            public string Id { get; }
            public string AlunoId { get; }
            public Turma Turma { get; }
            public DateTime InscritoEm { get; }

            internal static Resultado<Inscricao, Falha> Nova(string alunoId, Turma turma)
            {
                if (turma.VagasDisponiveis <= 0)
                    return Falha.Nova(400, "Sem vagas disponiveis");
                turma.IncrementarInscricao();
                return new Inscricao(Guid.NewGuid().ToString(), alunoId, turma, DateTime.Now);
            }
        }
    }
}
