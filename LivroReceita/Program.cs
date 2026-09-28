using LivroReceita.Views;
using LivroReceita.Views.ViewsIntroducao;
using System;
using System.Windows.Forms;

namespace LivroReceita
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (Properties.Settings.Default.isFirstAccess)
            {
                using (var frmIntro = new FrmIntroducao1())
                {
                    frmIntro.ShowDialog();
                }
            }

            Application.Run(new FrmLogin());
        }
    }
}

