using LivroReceita.Helpers;
using LivroReceita.Service;
using LivroReceita.Views.Cards;
using System;
using System.Windows.Forms;
using static LivroReceita.Service.ReceitasService;

namespace LivroReceita.Views
{
    public partial class FrmTelaPrincipal : Form
    {

        private readonly ReceitasService _receitasService = new ReceitasService();
        private readonly int _idUsuarioLogado = Sessao.UsuarioAtual.id;
        public FrmTelaPrincipal()
        {
            InitializeComponent();
        }

        private void FrmTelaPrincipal_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(TipoOrdenacao));
            comboBox1.SelectedIndex = 0;

            CarregarCards();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CarregarCards(string filtro = "")
        {

            TipoOrdenacao ordenacao = (TipoOrdenacao)comboBox1.SelectedItem;

            var receitas = _receitasService.ListarTodosReceitas(_idUsuarioLogado, filtro, ordenacao);

            flpReceitas.Controls.Clear();

            foreach (var receita in receitas)
            {
                var card = new UCCardReceita(receita);
                card.OnCardSelecionado += Card_OnCardSelecionado;
                card.OnFavoritoAlterado += Card_OnFavoritoAlterado;


                flpReceitas.Controls.Add(card);
            }
        }

        private void Card_OnFavoritoAlterado(object sender, EventArgs e)
        {

            var cardAlterado = (UCCardReceita)sender;

            _receitasService.AlternarReceitaFavorita(
                    Sessao.UsuarioAtual.id,
                    cardAlterado.ReceitaID,
                    cardAlterado.IsFavorita
                );
        }

        private void Card_OnCardSelecionado(object sender, int receitaId)
        {
            using (var frmDetalheReceita = new FrmDetalheReceita(receitaId))
            {
                frmDetalheReceita.ShowDialog();
            }
            CarregarCards();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CarregarCards(textBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarCards(textBox1.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FrmReceitasSalvas frmReceitasSalvas = new FrmReceitasSalvas();
            frmReceitasSalvas.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            using (var frmPerfil = new FrmPerfilAlimentar(_idUsuarioLogado))
            {
                frmPerfil.ShowDialog();
            }
            // Recarrega os cards porque o calculo de Gosto/Pontuação mudou
            CarregarCards(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Busca a receita sorteada
            var receitaAleatoria = _receitasService.ObterReceitaAleatoriaNaoSalva(_idUsuarioLogado);

            if (receitaAleatoria == null)
            {
                MessageBox.Show("Não há receitas não salvas disponíveis!", "SPSKILLS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Limpa a tela principal e exibe APENAS a receita sorteada no FlowLayoutPanel
            flpReceitas.Controls.Clear();

            var card = new UCCardReceita(receitaAleatoria);
            card.OnCardSelecionado += Card_OnCardSelecionado;
            card.OnFavoritoAlterado += Card_OnFavoritoAlterado;

            flpReceitas.Controls.Add(card);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Limpa o campo de texto
            textBox1.Clear();

            // 2. Reseta a ordenação para o primeiro item (Gosto / Padrão)
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            // 3. Recarrega a lista trazendo todas as receitas sem filtro
            CarregarCards();
        }
    }
}
