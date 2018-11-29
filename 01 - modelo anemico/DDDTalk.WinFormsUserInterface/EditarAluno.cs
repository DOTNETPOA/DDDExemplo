using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDDTalk.WinFormsUserInterface
{
    public partial class EditarAluno : Form
    {
        private string _id;
        public EditarAluno(string id)
        {
            InitializeComponent();
            _id = id;
            var aluno = Carregar(id);
            TextNome.Text = aluno.Nome;
            TextDataNascimento.Value = aluno.DataNascimento;
            TextEmail.Text = aluno.Email;
        }

        private void BotaoSalvar_Click(object sender, EventArgs e)
        {
            var aluno = AlunoEditadoModel.Novo(TextNome.Text, TextEmail.Text, TextDataNascimento.Value);
            var cliente = new RestClient("http://localhost:5000/api");
            var requisicao = new RestRequest("Alunos/{id}", Method.PUT);
            requisicao.AddUrlSegment("id", _id);
            requisicao.AddJsonBody(aluno);
            var resposta = cliente.Execute(requisicao);
            if (resposta.StatusCode != System.Net.HttpStatusCode.OK)
                MessageBox.Show($"Erro ao atualizar aluno: {resposta.Content}");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private AlunoEditadoModel Carregar(string id)
        {
            var cliente = new RestClient("http://localhost:5000/api");
            var requisicao = new RestRequest("Alunos/{id}", Method.GET);
            requisicao.AddUrlSegment("id", id);
            var resposta = cliente.Execute<List<AlunoEditadoModel>>(requisicao);
            if (resposta.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            return resposta.Data.FirstOrDefault();
        }

        public sealed class AlunoEditadoModel
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public DateTime DataNascimento { get; set; }

            public static AlunoEditadoModel Novo(string nome, string email, DateTime dataNascimento)
                => new AlunoEditadoModel
                {
                    Nome = nome,
                    Email = email,
                    DataNascimento = dataNascimento
                };
        }
    }
}
