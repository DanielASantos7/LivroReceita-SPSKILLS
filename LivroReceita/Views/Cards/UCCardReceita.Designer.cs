namespace LivroReceita.Views.Cards
{
    partial class UCCardReceita
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNomeReceita = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTempoPreparo = new System.Windows.Forms.Label();
            this.picSalvarReceita = new System.Windows.Forms.PictureBox();
            this.picFotoReceita = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSalvarReceita)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoReceita)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomeReceita
            // 
            this.lblNomeReceita.AutoSize = true;
            this.lblNomeReceita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeReceita.Location = new System.Drawing.Point(19, 169);
            this.lblNomeReceita.Name = "lblNomeReceita";
            this.lblNomeReceita.Size = new System.Drawing.Size(113, 16);
            this.lblNomeReceita.TabIndex = 1;
            this.lblNomeReceita.Text = "Nome da Receita";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Preparo";
            // 
            // lblTempoPreparo
            // 
            this.lblTempoPreparo.AutoSize = true;
            this.lblTempoPreparo.Location = new System.Drawing.Point(19, 221);
            this.lblTempoPreparo.Name = "lblTempoPreparo";
            this.lblTempoPreparo.Size = new System.Drawing.Size(35, 13);
            this.lblTempoPreparo.TabIndex = 3;
            this.lblTempoPreparo.Text = "x MIN";
            // 
            // picSalvarReceita
            // 
            this.picSalvarReceita.Location = new System.Drawing.Point(157, 198);
            this.picSalvarReceita.Name = "picSalvarReceita";
            this.picSalvarReceita.Size = new System.Drawing.Size(41, 52);
            this.picSalvarReceita.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSalvarReceita.TabIndex = 4;
            this.picSalvarReceita.TabStop = false;
            this.picSalvarReceita.Click += new System.EventHandler(this.picSalvarReceita_Click);
            // 
            // picFotoReceita
            // 
            this.picFotoReceita.Location = new System.Drawing.Point(15, 14);
            this.picFotoReceita.Name = "picFotoReceita";
            this.picFotoReceita.Size = new System.Drawing.Size(183, 145);
            this.picFotoReceita.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFotoReceita.TabIndex = 0;
            this.picFotoReceita.TabStop = false;
            // 
            // UCCardReceita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picSalvarReceita);
            this.Controls.Add(this.lblTempoPreparo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNomeReceita);
            this.Controls.Add(this.picFotoReceita);
            this.Name = "UCCardReceita";
            this.Size = new System.Drawing.Size(215, 258);
            ((System.ComponentModel.ISupportInitialize)(this.picSalvarReceita)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFotoReceita)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picFotoReceita;
        private System.Windows.Forms.Label lblNomeReceita;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTempoPreparo;
        private System.Windows.Forms.PictureBox picSalvarReceita;
    }
}
