namespace DDDTalk.WinFormsUserInterface
{
    partial class EditarAluno
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BotaoSalvar = new System.Windows.Forms.Button();
            this.TextEmail = new System.Windows.Forms.TextBox();
            this.TextNome = new System.Windows.Forms.TextBox();
            this.TextDataNascimento = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // BotaoSalvar
            // 
            this.BotaoSalvar.Location = new System.Drawing.Point(12, 110);
            this.BotaoSalvar.Name = "BotaoSalvar";
            this.BotaoSalvar.Size = new System.Drawing.Size(75, 23);
            this.BotaoSalvar.TabIndex = 7;
            this.BotaoSalvar.Text = "Salvar";
            this.BotaoSalvar.UseVisualStyleBackColor = true;
            this.BotaoSalvar.Click += new System.EventHandler(this.BotaoSalvar_Click);
            // 
            // TextEmail
            // 
            this.TextEmail.Location = new System.Drawing.Point(12, 67);
            this.TextEmail.Name = "TextEmail";
            this.TextEmail.Size = new System.Drawing.Size(356, 22);
            this.TextEmail.TabIndex = 6;
            // 
            // TextNome
            // 
            this.TextNome.Location = new System.Drawing.Point(12, 11);
            this.TextNome.Name = "TextNome";
            this.TextNome.Size = new System.Drawing.Size(356, 22);
            this.TextNome.TabIndex = 5;
            // 
            // TextDataNascimento
            // 
            this.TextDataNascimento.Location = new System.Drawing.Point(12, 39);
            this.TextDataNascimento.Name = "TextDataNascimento";
            this.TextDataNascimento.Size = new System.Drawing.Size(200, 22);
            this.TextDataNascimento.TabIndex = 4;
            // 
            // EditarAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 152);
            this.Controls.Add(this.BotaoSalvar);
            this.Controls.Add(this.TextEmail);
            this.Controls.Add(this.TextNome);
            this.Controls.Add(this.TextDataNascimento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditarAluno";
            this.Text = "Editar Aluno";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BotaoSalvar;
        private System.Windows.Forms.TextBox TextEmail;
        private System.Windows.Forms.TextBox TextNome;
        private System.Windows.Forms.DateTimePicker TextDataNascimento;
    }
}