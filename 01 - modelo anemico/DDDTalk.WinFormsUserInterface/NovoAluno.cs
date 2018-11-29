using Newtonsoft.Json;
using RestSharp;
using System;
using System.Windows.Forms;

namespace DDDTalk.WinFormsUserInterface
{
    public partial class NovoAluno : Form
    {
        public string IdIncluido { get; set; }

        public NovoAluno()
        {
            InitializeComponent();
        }

        private void BotaoSalvar_Click(object sender, EventArgs e)
        {
            var aluno = NovoAlunoModel.Novo(TextNome.Text, TextEmail.Text, TextDataNascimento.Value);
            var cliente = new RestClient("http://localhost:5000/api");
            var requisicao = new RestRequest("Alunos", Method.POST);
            requisicao.AddJsonBody(aluno);
            var resposta = cliente.Execute(requisicao);
            if (resposta.StatusCode != System.Net.HttpStatusCode.Created)
                MessageBox.Show($"Erro ao cadastrar aluno: {resposta.Content}");
            IdIncluido = JsonConvert.DeserializeObject<NovoAlunoModel>(resposta.Content).Id;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public sealed class NovoAlunoModel
        {
            public string Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public DateTime DataNascimento { get; set; }

            public static NovoAlunoModel Novo(string nome, string email, DateTime dataNascimento)
                => new NovoAlunoModel
                {
                    Id = "",
                    Nome = nome,
                    Email = email,
                    DataNascimento = dataNascimento
                };
        }
    }
}
