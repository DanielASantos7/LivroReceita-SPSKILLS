using LivroReceita.DTO;
using System.Windows.Forms;

namespace LivroReceita.Views.Cards
{
    public partial class UCCardIngredienteReceita : UserControl
    {
        public UCCardIngredienteReceita(IngredienteDetalheDTO dto)
        {
            InitializeComponent();

            label1.Text = dto.Nome;
            label2.Text = $"{dto.QtdGramas}g";
        }
    }
}
