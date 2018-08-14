using System.ComponentModel.DataAnnotations;

namespace DDDTalk.WebApi.Models
{
    public sealed class Turma
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Descrição muito grande")]
        public string Descricao { get; set; }
        [Required]
        [Range(1,99, ErrorMessage ="Limite de alunos deve ser entre 1 e 99")]
        public int LimiteAlunos { get; set; }
    }
}
