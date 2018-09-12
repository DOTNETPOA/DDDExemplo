using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("DDDTalk.Dominio.Infra.SqlServer.Dapper")]
namespace DDDTalk.Dominio
{    
    public sealed class Aluno
    {
        
        internal Aluno(string id, string nome, string email, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }

        public string Id { get; }
        public string Nome { get; }
        public string Email { get;  }
        public DateTime DataNascimento { get; }
        public int Idade(DateTime quando)
        {
            var idade = quando.Year - DataNascimento.Year;
            return quando < DataNascimento.AddYears(idade)
                ? idade--
                : idade;
        }

        public static Aluno Novo(string nome, string email, DateTime dataNascimento)
        {
            if (String.IsNullOrEmpty(nome) || (nome.Length <= 5 && nome.Length > 100))
                throw new InvalidOperationException("Nome deve ter 5 a 100 letras");
            email = (email ?? string.Empty).Trim();
            if (email.Length == 0)
                throw new InvalidOperationException("Email não pode ser vazio");
            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                throw new InvalidOperationException("Email inválido");
            if(dataNascimento >= DateTime.Now)
                throw new InvalidOperationException("Data de nascimento deve ser menor que hoje");
            return new Aluno(Guid.NewGuid().ToString(), nome, email, dataNascimento);
        }
    }
}
