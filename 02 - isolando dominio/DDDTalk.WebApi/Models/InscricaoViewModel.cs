using System;

namespace DDDTalk.WebApi.Models
{
    public sealed class InscricaoViewModel
    {
        public InscricaoViewModel(string alunoId, string turmaId, DateTime inscritoEm)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
            InscritoEm = inscritoEm;
        }

        public string AlunoId { get; }
        public string TurmaId { get; }
        public DateTime InscritoEm { get; }
    }
}
