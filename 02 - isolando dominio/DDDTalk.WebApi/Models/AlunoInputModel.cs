using System;

namespace DDDTalk.WebApi.Models
{
    public sealed class AlunoInputModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public sealed class AlunoViewModel
    {
        public AlunoViewModel(string id, string nome, string email, int idade)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Idade = idade;
        }

        public string Id { get; }
        public string Nome { get; }
        public string Email { get; }
        public int Idade { get; }
    }
}
