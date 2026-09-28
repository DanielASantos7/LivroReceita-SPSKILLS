using LivroReceita.Helpers;
using LivroReceita.Service;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LivroReceita.Views
{
    public partial class FrmLogin : Form
    {

        private readonly AuthService _authService = new AuthService();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.EndsWith("@"))
            {
                textBox1.Text += "email.com";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = default;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!validacaoes())
            {
                return;
            }


            var res = _authService.LoginAuth(textBox1.Text, textBox2.Text);

            if (res == null)
            {
                MessageBox.Show("Credenciais inválidas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Login efetuado com sucesso, você será direcinado para a tela principal", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Sessao.UsuarioAtual = res;

            FrmTelaPrincipal frmTelaPrincipal = new FrmTelaPrincipal();
            frmTelaPrincipal.Show();
            this.Hide();

        }


        private bool validacaoes()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, preencha seu email", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!textBox1.Text.Contains("@email.com"))
            {
                MessageBox.Show("Seu email deve conter @email.com", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBox2.Text.Length <= 7)
            {
                MessageBox.Show("Sua senha deve ter no mínimo 8 caracteres!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Insira sua senha!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (checkBox1.Checked)
            {
                Properties.Settings.Default.emailSaved = textBox1.Text.Trim();
                Properties.Settings.Default.passwordSaved = textBox2.Text.Trim();
                Properties.Settings.Default.checkboxChecked = true;
                Properties.Settings.Default.Save();

            }
            else
            {
                Properties.Settings.Default.emailSaved = string.Empty;
                Properties.Settings.Default.passwordSaved = string.Empty;
                Properties.Settings.Default.checkboxChecked = false;
                Properties.Settings.Default.Save();

            }

            return true;
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Y)
            {
                Properties.Settings.Default.isFirstAccess = true;
                Properties.Settings.Default.emailSaved = string.Empty;
                Properties.Settings.Default.passwordSaved = string.Empty;

                Properties.Settings.Default.Save();
                Application.Restart();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            using (FrmCadastro frmCadastro = new FrmCadastro())
            {
                if (frmCadastro.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = frmCadastro.EmailCadastrado;
                    textBox2.Text = frmCadastro.SenhaCadastrada;

                    MessageBox.Show($"Bem-vindo, {frmCadastro.NomeCadastrado}! Seu email e senha foram preenchido automaticamente.");
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.isFirstAccess = false;
            textBox1.Text = Properties.Settings.Default.emailSaved;
            textBox2.Text = Properties.Settings.Default.passwordSaved;
            checkBox1.Checked = Properties.Settings.Default.checkboxChecked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirReadmeLocal();
        }

        private void AbrirReadmeLocal()
        {
            try
            {
                string pastaBase = Application.StartupPath;

                // Lista de possíveis nomes de arquivos de instrução
                string[] arquivosPossiveis = {
            "README.txt",
            "README.md",
            "00_LEIA-ME_PRIMEIRO.txt",
            "README.txt.txt"
        };

                string caminhoEncontrado = null;

                foreach (var nomeArquivo in arquivosPossiveis)
                {
                    // 1. Procura na pasta do executável (bin/Debug ou bin/Release)
                    string caminhoDireto = Path.Combine(pastaBase, nomeArquivo);
                    if (File.Exists(caminhoDireto))
                    {
                        caminhoEncontrado = caminhoDireto;
                        break;
                    }

                    // 2. Fallback: Procura na pasta raiz do projeto (útil durante o desenvolvimento em F5)
                    DirectoryInfo diretorioPai = Directory.GetParent(pastaBase);
                    while (diretorioPai != null)
                    {
                        string caminhoNoPai = Path.Combine(diretorioPai.FullName, nomeArquivo);
                        if (File.Exists(caminhoNoPai))
                        {
                            caminhoEncontrado = caminhoNoPai;
                            break;
                        }
                        diretorioPai = diretorioPai.Parent;
                    }

                    if (caminhoEncontrado != null) break;
                }

                if (caminhoEncontrado != null)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = caminhoEncontrado,
                        UseShellExecute = true // Abre com o aplicativo padrão do SO (Bloco de Notas / Navegador)
                    });
                }
                else
                {
                    MessageBox.Show(
                        $"O arquivo de instruções não foi encontrado na pasta da aplicação:\n{pastaBase}\n\n" +
                        "Verifique se o arquivo existe na pasta e se a propriedade 'Copy to Output Directory' está como 'Copy if newer'.",
                        "Manual Não Encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir o manual: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
