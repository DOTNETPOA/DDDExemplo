using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DDDTalk.WinFormsUserInterface
{
    public partial class FormPrincipal : Form
    {
        private IList<Form> _janelasAbertas;

        public FormPrincipal()
        {
            InitializeComponent();
            _janelasAbertas = new List<Form>();
        }

        private void alunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string nomeJanela = "ListagemAlunos";
            if (!_janelasAbertas.Any(f => f.Name == nomeJanela))
            {
                var form = new ListagemAlunos { MdiParent = this };
                form.FormClosing += ListagemAlunoFechando;
                _janelasAbertas.Add(form);
            }
            _janelasAbertas.FirstOrDefault(f => f.Name == nomeJanela).Show();
        }

        private void ListagemAlunoFechando(object sender, FormClosingEventArgs e)
        {
            _janelasAbertas.Remove((ListagemAlunos)sender);
        }
    }
}
