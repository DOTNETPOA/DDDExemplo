namespace DDDTalk.WinFormsUserInterface
{
    partial class NovoAluno
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
            this.TextDataNascimento = new System.Windows.Forms.DateTimePicker();
            this.TextNome = new System.Windows.Forms.TextBox();
            this.TextEmail = new System.Windows.Forms.TextBox();
            this.BotaoSalvar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextDataNascimento
            // 
            this.TextDataNascimento.Location = new System.Drawing.Point(41, 40);
            this.TextDataNascimento.Name = "TextDataNascimento";
            this.TextDataNascimento.Size = new System.Drawing.Size(200, 22);
            this.TextDataNascimento.TabIndex = 0;
            // 
            // TextNome
            // 
            this.TextNome.Location = new System.Drawing.Point(41, 12);
            this.TextNome.Name = "TextNome";
            this.TextNome.Size = new System.Drawing.Size(356, 22);
            this.TextNome.TabIndex = 1;
            // 
            // TextEmail
            // 
            this.TextEmail.Location = new System.Drawing.Point(41, 68);
            this.TextEmail.Name = "TextEmail";
            this.TextEmail.Size = new System.Drawing.Size(356, 22);
            this.TextEmail.TabIndex = 2;
            // 
            // BotaoSalvar
            // 
            this.BotaoSalvar.Location = new System.Drawing.Point(41, 111);
            this.BotaoSalvar.Name = "BotaoSalvar";
            this.BotaoSalvar.Size = new System.Drawing.Size(75, 23);
            this.BotaoSalvar.TabIndex = 3;
            this.BotaoSalvar.Text = "Salvar";
            this.BotaoSalvar.UseVisualStyleBackColor = true;
            this.BotaoSalvar.Click += new System.EventHandler(this.BotaoSalvar_Click);
            // 
            // NovoAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 158);
            this.Controls.Add(this.BotaoSalvar);
            this.Controls.Add(this.TextEmail);
            this.Controls.Add(this.TextNome);
            this.Controls.Add(this.TextDataNascimento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NovoAluno";
            this.Text = "Novo Aluno";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker TextDataNascimento;
        private System.Windows.Forms.TextBox TextNome;
        private System.Windows.Forms.TextBox TextEmail;
        private System.Windows.Forms.Button BotaoSalvar;
    }
}