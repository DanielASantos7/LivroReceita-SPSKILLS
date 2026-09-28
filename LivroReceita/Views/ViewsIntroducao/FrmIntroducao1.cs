using System.Windows.Forms;

namespace LivroReceita.Views.ViewsIntroducao
{
    public partial class FrmIntroducao1 : Form
    {
        public FrmIntroducao1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            using (var frm2 = new FrmIntroducao2())
            {
                var resultado = frm2.ShowDialog();

                // Se o usuario clicou em 'Voltar' na Tela 2, reexibe a Tela 1
                if (resultado == DialogResult.Retry)
                {
                    this.Show();
                }
                else
                {
                    // Se concluiu ou fechou a cadeia, encerra a Tela 1
                    this.Close();
                }
            }
        }
    }
}
