using LivroReceita.Helpers;
using LivroReceita.Service;
using LivroReceita.Views.Cards;
using System;
using System.Windows.Forms;
using static LivroReceita.Service.ReceitasService;

namespace LivroReceita.Views
{
    public partial class FrmReceitasSalvas : Form
    {
        private readonly ReceitasService _receitasService = new ReceitasService();
        private readonly int _idUsuarioLogado = Sessao.UsuarioAtual.id;

        public FrmReceitasSalvas()
        {
            InitializeComponent();
        }

        private void FrmReceitasSalvas_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(TipoOrdenacao));
            comboBox1.SelectedIndex = 0;

            CarregarCards();
        }

        private void CarregarCards(string filtro = "")
        {
            TipoOrdenacao ordenacao = (TipoOrdenacao)comboBox1.SelectedItem;

            var receitas = _receitasService.ListarTodosReceitas(_idUsuarioLogado, filtro, ordenacao, true);

            flpReceitas.Controls.Clear();

            foreach (var receita in receitas)
            {
                var card = new UCCardReceita(receita);
                card.OnCardSelecionado += Card_OnCardSelecionado;
                card.OnFavoritoAlterado += Card_OnFavoritoAlterado;


                flpReceitas.Controls.Add(card);
            }
        }

        private void Card_OnCardSelecionado(object sender, int receitaId)
        {
            using (var frmDetalheReceita = new FrmDetalheReceita(receitaId))
            {
                frmDetalheReceita.ShowDialog();
            }
            CarregarCards();
        }

        private void Card_OnFavoritoAlterado(object sender, EventArgs e)
        {
            var cardAlterado = (UCCardReceita)sender;

            _receitasService.AlternarReceitaFavorita(
                    Sessao.UsuarioAtual.id,
                    cardAlterado.ReceitaID,
                    cardAlterado.IsFavorita
                );

            CarregarCards();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CarregarCards(textBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarCards(textBox1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FrmTelaPrincipal frmTelaPrincipal = new FrmTelaPrincipal();
            frmTelaPrincipal.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            using (var frmPerfil = new FrmPerfilAlimentar(_idUsuarioLogado))
            {
                frmPerfil.ShowDialog();
            }
            CarregarCards(textBox1.Text);
        }

    }
}
