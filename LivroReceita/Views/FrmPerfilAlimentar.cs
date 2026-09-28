using LivroReceita.DTO;
using LivroReceita.Service;
using LivroReceita.Views.Cards;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LivroReceita.Views
{
    public partial class FrmPerfilAlimentar : Form
    {
        private readonly PerfilAlimentarService _service = new PerfilAlimentarService();
        private readonly int _usuarioId;
        private List<PerfilAlimentarDTO> _listaItens;

        public FrmPerfilAlimentar(int usuarioId)
        {
            InitializeComponent();
            _usuarioId = usuarioId;
        }

        private void FrmPerfilAlimentar_Load(object sender, EventArgs e)
        {
            ConfigurarDragDrop();
            CarregarDados();
        }

        private void ConfigurarDragDrop()
        {
            // 1. Habilita a recepção de arrasto nos 3 painéis reais
            flpTodosIngredientes.AllowDrop = true;
            flpApreciados.AllowDrop = true;
            flpEvitados.AllowDrop = true;

            // 2. Associa a regra de soltar o card em cada painel
            RegistrarDrop(flpTodosIngredientes, null);   // Indiferente/Geral
            RegistrarDrop(flpApreciados, true);          // Apreciado
            RegistrarDrop(flpEvitados, false);           // Evitado
        }

        private void RegistrarDrop(FlowLayoutPanel panel, bool? novoStatus)
        {
            panel.DragEnter += (s, e) =>
            {
                if (e.Data.GetDataPresent(typeof(UCCardPerfilAlimentar)))
                    e.Effect = DragDropEffects.Move;
            };

            panel.DragDrop += (s, e) =>
            {
                var card = e.Data.GetData(typeof(UCCardPerfilAlimentar)) as UCCardPerfilAlimentar;
                if (card != null)
                {
                    MoverCard(card, panel, novoStatus);
                }
            };
        }

        private void CarregarDados()
        {
            _listaItens = _service.ListarTodosIngrediente(_usuarioId);
            Renderizar();
        }

        private void Renderizar()
        {
            // Limpa os 3 painéis
            flpTodosIngredientes.Controls.Clear();
            flpApreciados.Controls.Clear();
            flpEvitados.Controls.Clear();

            string busca = textBox1 != null ? textBox1.Text.ToLower().Trim() : "";

            foreach (var item in _listaItens)
            {
                var card = new UCCardPerfilAlimentar(item);

                // Arrasta ao clicar com botão esquerdo
                card.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        card.DoDragDrop(card, DragDropEffects.Move);
                };

                // Duplo clique move direto sem arrastar
                card.DoubleClick += (s, e) => AlternarComDuploClique(card);

                // Triagem para o painel correto
                if (item.Status == true)
                {
                    flpApreciados.Controls.Add(card);
                }
                else if (item.Status == false)
                {
                    flpEvitados.Controls.Add(card);
                }
                else
                {
                    if (string.IsNullOrEmpty(busca) || item.NomeIngrediente.ToLower().Contains(busca))
                        flpTodosIngredientes.Controls.Add(card);
                }
            }
        }

        private void AlternarComDuploClique(UCCardPerfilAlimentar card)
        {
            // Transição rápida: Todos (null) -> Apreciados (true) -> Evitados (false) -> Todos (null)
            if (card.ItemDTO.Status == null)
            {
                MoverCard(card, flpApreciados, true);
            }
            else if (card.ItemDTO.Status == true)
            {
                MoverCard(card, flpEvitados, false);
            }
            else
            {
                MoverCard(card, flpTodosIngredientes, null);
            }
        }

        private void MoverCard(UCCardPerfilAlimentar card, FlowLayoutPanel destino, bool? novoStatus)
        {
            card.ItemDTO.Status = novoStatus; // Altera no DTO em memória
            destino.Controls.Add(card);       // O WinForms move o controle pro painel de destino
        }

        private void textBox1_TextChanged(object sender, EventArgs e) => Renderizar();
        private void button2_Click(object sender, EventArgs e)
        {
            _service.SalvarPerfilAlimentar(_usuarioId, _listaItens);
            MessageBox.Show("Perfil salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}