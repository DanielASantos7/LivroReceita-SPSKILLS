using LivroReceita.Helpers;
using LivroReceita.Service;
using LivroReceita.Views.Cards;
using System.Windows.Forms;

namespace LivroReceita.Views
{
    public partial class FrmDetalheReceita : Form
    {
        private readonly int idReceitaSelecionada;
        private readonly ReceitasService _receitaService = new ReceitasService();
        public FrmDetalheReceita(int idReceita)
        {
            InitializeComponent();
            idReceitaSelecionada = idReceita;
        }

        private void FrmDetalheReceita_Load(object sender, System.EventArgs e)
        {
            CarregarDetalhes();
        }


        private void CarregarDetalhes()
        {
            var detalhes = _receitaService.DetalhesReceita(idReceitaSelecionada);

            if (detalhes == null)
            {
                MessageBox.Show("Receita não encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                return;
            }

            lblNomeReceita.Text = detalhes.Nome;
            lblAutor.Text = detalhes.Autor;
            lblTempoPreparo.Text = $"{detalhes.TempoPreparo} min";
            lblSaves.Text = $"{detalhes.QtdVezesSalvas} Saves";
            pictureBox1.Image = pictureBox1.Image = ImagemHelper.CarregarImagemReceita(detalhes.Id);
            flpIngredientes.Controls.Clear();

            foreach (var ing in detalhes.Ingredientes)
            {
                var cardIngrediente = new UCCardIngredienteReceita(ing);
                flpIngredientes.Controls.Add(cardIngrediente);
            }

            flpProcedimentos.Controls.Clear();

            foreach (var proc in detalhes.Procedimentos)
            {
                var cardProcedimentos = new UCCardProcedimentoReceita(proc);
                flpProcedimentos.Controls.Add(cardProcedimentos);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
