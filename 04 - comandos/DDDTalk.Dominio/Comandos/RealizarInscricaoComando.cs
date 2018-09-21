namespace DDDTalk.Dominio.Comandos
{
    public sealed class RealizarInscricaoComando
    {
        public RealizarInscricaoComando(string alunoId, string turmaId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }

        public string AlunoId { get; }
        public string TurmaId { get; }
    }
}
