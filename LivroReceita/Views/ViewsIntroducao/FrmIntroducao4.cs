using System;
using System.Windows.Forms;

namespace LivroReceita.Views.ViewsIntroducao
{
    public partial class FrmIntroducao4 : Form
    {
        public FrmIntroducao4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Grava a propriedade de primeiro acesso como concluida
            Properties.Settings.Default.isFirstAccess = false;
            Properties.Settings.Default.Save();

            // 2. Define o resultado de encerramento do Onboarding
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
    }
}
