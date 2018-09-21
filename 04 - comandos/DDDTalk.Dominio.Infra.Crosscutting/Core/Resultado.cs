using System;

namespace DDDTalk.Dominio.Infra.Crosscutting.Core
{
    public struct Resultado<TSucesso, TFalha>
    {

        internal Resultado(TSucesso sucesso)
        {
            EhFalha = false;
            Sucesso = sucesso;
            Falha = default(TFalha);
        }

        internal Resultado(TFalha falha)
        {
            EhFalha = true;
            Sucesso = default(TSucesso);
            Falha = falha;
        }

        public TSucesso Sucesso { get; }
        public TFalha Falha { get; }
        public bool EhFalha { get; }
        public bool EhSucesso => !EhFalha;

        public static implicit operator Resultado<TSucesso, TFalha>(TFalha falha)
            => new Resultado<TSucesso, TFalha>(falha);

        public static implicit operator Resultado<TSucesso, TFalha>(TSucesso sucesso)
            => new Resultado<TSucesso, TFalha>(sucesso);

        public static Resultado<TSucesso, TFalha> NovoSucesso(TSucesso objeto) => objeto;
        public static Resultado<TSucesso, TFalha> NovaFalha(TFalha objeto) => objeto;

        public override bool Equals(object obj)
        {
            try
            {
                if (!(obj is Resultado<TSucesso, TFalha>)) return false;
                var other = (Resultado<TSucesso, TFalha>)obj;
                return EhSucesso
                        ? Sucesso.Equals(other.Sucesso)
                        : Falha.Equals(other.Falha);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
            => EhSucesso
                    ? Sucesso.GetHashCode()
                    : Falha.GetHashCode();

        public static bool operator ==(Resultado<TSucesso, TFalha> a, Resultado<TSucesso, TFalha> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Resultado<TSucesso, TFalha> a, Resultado<TSucesso, TFalha> b)
            => !(a == b);

    }
}
