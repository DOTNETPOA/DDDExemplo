using System;

namespace DDDTalk.WebApi.Models
{
    public sealed class InscricaoViewModel
    {
        public InscricaoViewModel(string alunoId, TurmaViewModel turma, DateTime inscritoEm)
        {
            AlunoId = alunoId;
            Turma = turma;
            InscritoEm = inscritoEm;
        }

        public string AlunoId { get; }
        public TurmaViewModel Turma { get; }
        public DateTime InscritoEm { get; }

        public sealed class TurmaViewModel
        {
            public TurmaViewModel(string id, string descricao)
            {
                Id = id;
                Descricao = descricao;
            }

            public string Id { get;  }
            public string Descricao { get; }
        }
    }
}
