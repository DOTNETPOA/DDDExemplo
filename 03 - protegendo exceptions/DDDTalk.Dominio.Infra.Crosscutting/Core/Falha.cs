using System;

namespace DDDTalk.Dominio.Infra.Crosscutting.Core
{
    public sealed class Falha : ValueObject<Falha>
    {
        public Falha(int codigo, string mensagem, Falha erroInterno)
        {
            Codigo = codigo;
            Mensagem = mensagem;
            ErroInterno = erroInterno;
        }

        public int Codigo { get; }
        public string Mensagem { get; }
        public Falha ErroInterno { get; }

        public static implicit operator Falha(Exception exception)
            => new Falha(400, exception.Message, null);

        public static Falha Nova(int codigo, string mensagem)
            => new Falha(codigo, mensagem, null);

        public static Falha Nova(int codigo, string mensagem, Falha erroInterno)
            => new Falha(codigo, mensagem, erroInterno);

        public static Falha NovaComException(Exception exception) => exception;

        protected override bool EqualsCore(Falha other)
        {
            var erroInterno = ErroInterno == null
                                ? (other.ErroInterno == null)
                                : ErroInterno.Equals(other.ErroInterno);
            return Codigo.Equals(other.Codigo) &&
                    Mensagem.Equals(other.Mensagem) &&
                    erroInterno;
        }

        protected override int GetHashCodeCore()
        {
            var erroInterno = ErroInterno == null
                                ? 0
                                : ErroInterno.GetHashCode();
            return Codigo.GetHashCode() ^
                    Mensagem.GetHashCode() ^
                    erroInterno;
        }
    }
}
