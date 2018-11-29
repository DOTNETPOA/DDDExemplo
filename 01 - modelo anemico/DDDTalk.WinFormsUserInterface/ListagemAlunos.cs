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
    public partial class ListagemAlunos : Form
    {
        public ListagemAlunos()
        {
            InitializeComponent();
        }

        private void ListagemAlunos_Load(object sender, EventArgs e)
        {
            CarregarAlunos();
        }

        public sealed class AlunoLista
        {
            public string Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public DateTime DataNascimento { get; set; }
        }

        private void BotaoNovo_Click(object sender, EventArgs e)
        {
            var novoAluno = new NovoAluno();
            var resultado = novoAluno.ShowDialog();
            if (resultado != DialogResult.OK)
                return;
            CarregarAlunos();
            DataGrid.ClearSelection();
            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                if ((string)DataGrid.Rows[i].Cells["Id"].Value == novoAluno.IdIncluido)
                    DataGrid.Rows[i].Selected = true;
            }
        }

        private void BotaoEditar_Click(object sender, EventArgs e)
        {
            var id = (string)DataGrid.SelectedRows[0].Cells["id"].Value;
            var editarAluno = new EditarAluno(id);
            var resultado = editarAluno.ShowDialog();
            if (resultado != DialogResult.OK)
                return;
            CarregarAlunos();
            DataGrid.ClearSelection();
            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                if ((string)DataGrid.Rows[i].Cells["Id"].Value == id)
                    DataGrid.Rows[i].Selected = true;
            }
        }

        private void CarregarAlunos()
        {
            var cliente = new RestClient("http://localhost:5000/api");
            var requisicao = new RestRequest("Alunos");
            var resposta = cliente.Execute<List<AlunoLista>>(requisicao);
            var binding = new BindingList<AlunoLista>(resposta.Data);
            DataGrid.DataSource = new BindingSource(binding, null);
        }
    }
}
