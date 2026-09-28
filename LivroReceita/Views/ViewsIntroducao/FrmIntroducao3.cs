using System;
using System.Windows.Forms;

namespace LivroReceita.Views.ViewsIntroducao
{
    public partial class FrmIntroducao3 : Form
    {
        public FrmIntroducao3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var frm4 = new FrmIntroducao4())
            {
                var resultado = frm4.ShowDialog();

                if (resultado == DialogResult.Retry)
                {
                    this.Show();
                }
                else
                {
                    this.DialogResult = resultado;
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
    }
}
