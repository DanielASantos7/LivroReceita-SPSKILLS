using LivroReceita.DTO;
using System.Windows.Forms;

namespace LivroReceita.Views.Cards
{
    public partial class UCCardProcedimentoReceita : UserControl
    {
        public UCCardProcedimentoReceita(ProcedimentoDetalheDTO dto)
        {
            InitializeComponent();
            label1.Text = $"{dto.Sequencia}";
            textBox1.Text = dto.Descricao;
        }
    }
}
