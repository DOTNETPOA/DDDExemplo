using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DDDTalk.Dominio.Infra.SqlServer.Dapper")]
namespace DDDTalk.Dominio
{
    public sealed class Turma
    {
        internal Turma(string id, string descricao, int limiteAlunos, int totalInscritos)
        {
            Id = id;
            Descricao = descricao;
            LimiteAlunos = limiteAlunos;
            TotalInscritos = totalInscritos;
        }

        public string Id { get; }
        public string Descricao { get; }
        public int LimiteAlunos { get;  }
        public int TotalInscritos { get;  }
        public int VagasDisponiveis => LimiteAlunos - TotalInscritos;

        public static Turma Nova(string descricao, int limiteAlunos)
        {
            if (String.IsNullOrEmpty(descricao) || (descricao.Length <= 5 && descricao.Length > 100))
                throw new InvalidOperationException("Descrição deve ter 5 a 100 letras");
            if(limiteAlunos <=0 || limiteAlunos > 100)
                throw new InvalidOperationException("Limite de alunos deve ser entre 1 e 99");
            return new Turma(Guid.NewGuid().ToString(), descricao, limiteAlunos, 0);
        }
    }
}
