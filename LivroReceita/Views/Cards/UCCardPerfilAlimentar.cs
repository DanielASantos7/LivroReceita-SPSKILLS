using LivroReceita.DTO;
using LivroReceita.Helpers;
using System.Windows.Forms;

namespace LivroReceita.Views.Cards
{
    public partial class UCCardPerfilAlimentar : UserControl
    {
        // Propriedade pública obrigatória para a Form ler/atualizar o DTO em memória
        public PerfilAlimentarDTO ItemDTO { get; private set; }

        public UCCardPerfilAlimentar()
        {
            InitializeComponent();
        }

        public UCCardPerfilAlimentar(PerfilAlimentarDTO dto) : this()
        {
            SetDados(dto);
        }

        /// <summary>
        /// Mapeia os dados do DTO para a interface e vincula a propagação de eventos.
        /// </summary>
        public void SetDados(PerfilAlimentarDTO dto)
        {
            ItemDTO = dto;
            label3.Text = dto.NomeIngrediente;
            pictureBox1.Image = ImagemHelper.CarregarImagemIngrediente(dto.NomeIngrediente);
            // Repassa os cliques no Label1 para o UserControl principal
            PropagarEventosChildControls();
        }

        /// <summary>
        /// Garante que se o usuário clicar/arrastar em cima do Label, o Card responda normalmente.
        /// </summary>
        private void PropagarEventosChildControls()
        {
            label3.MouseDown += (s, e) => OnMouseDown(e);
            label3.DoubleClick += (s, e) => OnDoubleClick(e);
        }
    }
}