namespace DDDTalk.WebApi.Models
{
    public sealed class TurmaViewModel
    {
        public TurmaViewModel(string id, string descricao, int limiteIdade, int vagasDisponiveis)
        {
            Id = id;
            Descricao = descricao;
            LimiteIdade = limiteIdade;
            VagasDisponiveis = vagasDisponiveis;
        }

        public string Id { get;  }
        public string Descricao { get;  }
        public int LimiteIdade { get; }
        public int VagasDisponiveis { get;  }
    }
}
