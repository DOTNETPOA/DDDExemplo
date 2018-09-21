namespace DDDTalk.WebApi.Models
{
    public sealed class TurmaViewModel
    {
        public TurmaViewModel(string id, string descricao, int vagasDisponiveis)
        {
            Id = id;
            Descricao = descricao;
            VagasDisponiveis = vagasDisponiveis;
        }

        public string Id { get;  }
        public string Descricao { get;  }
        public int VagasDisponiveis { get;  }
    }
}
