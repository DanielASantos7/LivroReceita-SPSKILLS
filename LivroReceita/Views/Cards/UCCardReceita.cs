using LivroReceita.DTO;
using LivroReceita.Helpers;
using System;
using System.Windows.Forms;

namespace LivroReceita.Views.Cards
{
    public partial class UCCardReceita : UserControl
    {

        public bool IsFavorita { get; set; }

        // Eventos para o formPai
        public event EventHandler OnFavoritoAlterado;
        public event EventHandler<int> OnCardSelecionado;

        public int ReceitaID { get; set; }


        public UCCardReceita(ReceitaDTO dto)
        {
            InitializeComponent();
            this.ReceitaID = dto.ID;
            lblNomeReceita.Text = dto.Nome;
            lblTempoPreparo.Text = $"{dto.TempoPreparo} min";
            this.IsFavorita = dto.IsFavorita;
            picFotoReceita.Image = ImagemHelper.CarregarImagemReceita(dto.ID);
            AtualizarFavorito();
            ConfigurarEventosDeClique(this);
        }

        private void AtualizarFavorito()
        {
            picSalvarReceita.Image = IsFavorita ? Properties.Resources.salvarChecked : Properties.Resources.salvar;
        }

        private void ConfigurarEventosDeClique(Control controlPai)
        {
            foreach (Control ctrl in controlPai.Controls)
            {
                if (ctrl == picSalvarReceita) continue;

                ctrl.Click -= ElementoCard_Click;
                ctrl.Click += ElementoCard_Click;

                if (ctrl.HasChildren)
                {
                    ConfigurarEventosDeClique(ctrl);
                }
            }
            controlPai.Click -= ElementoCard_Click;
            controlPai.Click += ElementoCard_Click;
        }

        private void ElementoCard_Click(object sender, EventArgs e)
        {
            OnCardSelecionado?.Invoke(this, ReceitaID);
        }

        private void picSalvarReceita_Click(object sender, EventArgs e)
        {
            IsFavorita = !IsFavorita;
            AtualizarFavorito();

            OnFavoritoAlterado?.Invoke(this, EventArgs.Empty);
        }
    }
}
