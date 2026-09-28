using LivroReceita.Service;
using System.Windows.Forms;

namespace LivroReceita.Views
{
    public partial class FrmCadastro : Form
    {
        private readonly AuthService _authService = new AuthService();

        public string EmailCadastrado { get; private set; }
        public string SenhaCadastrada { get; private set; }
        public string NomeCadastrado { get; private set; }

        public FrmCadastro()
        {
            InitializeComponent();
        }


        private bool ValidacoesCadastro()
        {
            // NOME COMPLETO
            if (txtNomeCompleto.Text.Trim().Split(' ').Length < 2 || string.IsNullOrWhiteSpace(txtNomeCompleto.Text))
            {
                MessageBox.Show("Por favor, coloque seu nome completo!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // EMAIL
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, insira seu email", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!textBox1.Text.Trim().EndsWith("@email.com"))
            {
                MessageBox.Show("Por favor, insira um email valido '@email.com'", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, insira sua senha!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (textBox2.Text.Length <= 7)
            {
                MessageBox.Show("Sua senha deve conter 8 ou mais caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var usuarioCadastrado = _authService.CadastroUsuario(txtNomeCompleto.Text, textBox1.Text, textBox2.Text);

            if (usuarioCadastrado == null)
            {
                MessageBox.Show("Já existe um usuário cadastrado com esse email, por favor, insira outro email!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;

        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            if (!ValidacoesCadastro())
            {
                return;
            }

            this.NomeCadastrado = txtNomeCompleto.Text.Trim();
            this.EmailCadastrado = textBox1.Text.Trim();
            this.SenhaCadastrada = textBox2.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
