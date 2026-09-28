using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LivroReceita.Helpers
{
    public static class ImagemHelper
    {
        private static string BasePath => Path.Combine(Application.StartupPath, "Resources");

        /// <summary>
        /// Carrega a foto da receita buscando EXCLUSIVAMENTE em 'users recipes images' pelo ID
        /// </summary>
        public static Image CarregarImagemReceita(int receitaId)
        {
            return BuscarImagemStream("users recipes images", receitaId.ToString()) ?? CarregarImagemPadrao();
        }

        /// <summary>
        /// Carrega a foto do ingrediente buscando em 'ingredientes' pelo NOME
        /// </summary>
        public static Image CarregarImagemIngrediente(string nomeIngrediente)
        {
            if (string.IsNullOrWhiteSpace(nomeIngrediente))
                return CarregarImagemPadrao();

            string nomeLimpo = nomeIngrediente.Trim().ToLower();
            return BuscarImagemStream("ingredientes", nomeLimpo) ?? CarregarImagemPadrao();
        }

        /// <summary>
        /// Retorna a imagem padrao do sistema
        /// </summary>
        public static Image CarregarImagemPadrao()
        {
            return BuscarImagemStream("", "imagem_padrao");
        }

        private static Image BuscarImagemStream(string subpasta, string nomeArquivo)
        {
            try
            {
                string diretorio = string.IsNullOrEmpty(subpasta)
                    ? BasePath
                    : Path.Combine(BasePath, subpasta);

                if (Directory.Exists(diretorio))
                {
                    string[] extensoes = { ".png", ".jpg", ".jpeg" };

                    foreach (var ext in extensoes)
                    {
                        string caminhoCompleto = Path.Combine(diretorio, nomeArquivo + ext);

                        if (File.Exists(caminhoCompleto))
                        {
                            using (var stream = new FileStream(caminhoCompleto, FileMode.Open, FileAccess.Read))
                            {
                                return Image.FromStream(stream);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Tratamento silencioso
            }

            return null;
        }
    }
}