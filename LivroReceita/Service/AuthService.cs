using LivroReceita.Data;
using System.Linq;

namespace LivroReceita.Service
{
    public class AuthService
    {

        // Serviço de login

        public Usuario LoginAuth(string email, string password)
        {
            using (var db = new dbLivroReceita())
            {
                var existUsuario = db.Usuario
                                     .Where(u => u.email == email && u.senha == password)
                                     .FirstOrDefault();

                if (existUsuario == null)
                {
                    return null;
                }

                return existUsuario;

            }
        }


        public Usuario CadastroUsuario(string nomeCompleto, string email, string password)
        {
            using (var db = new dbLivroReceita())
            {
                var usuarioExiste = db.Usuario
                                      .FirstOrDefault(u => u.email == email);

                if (usuarioExiste == null)
                {
                    var novoUsuario = new Usuario
                    {
                        nome = nomeCompleto,
                        email = email,
                        senha = password
                    };

                    db.Usuario.Add(novoUsuario);
                    db.SaveChanges();

                    return novoUsuario;
                }
                return null;
            }
        }
    }
}
