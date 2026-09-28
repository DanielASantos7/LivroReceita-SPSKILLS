using System;
using System.Windows.Forms;

namespace LivroReceita.Views.ViewsIntroducao
{
    public partial class FrmIntroducao2 : Form
    {
        public FrmIntroducao2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var frm3 = new FrmIntroducao3())
            {
                var resultado = frm3.ShowDialog();

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
