using System;
using System.ComponentModel.DataAnnotations;

namespace DDDTalk.WebApi.Models
{
    public sealed class Inscricao
    {
        public string Id { get; set; }
        public string AlunoId { get; set; }
        public DateTime InscritoEm { get; set; }
        [Required]
        public string TurmaId { get; set; }
    }
}
