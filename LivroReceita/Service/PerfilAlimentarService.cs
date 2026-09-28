using LivroReceita.Data;
using LivroReceita.DTO;
using System.Collections.Generic;
using System.Linq;

namespace LivroReceita.Service
{
    public class PerfilAlimentarService
    {
        // 1. Busca todos os ingredientes e cruza o status salvo do usuário
        public List<PerfilAlimentarDTO> ListarTodosIngrediente(int idUsuario)
        {
            using (var db = new dbLivroReceita())
            {
                var todosIngredientes = db.Ingrediente.AsNoTracking().ToList();
                var perfisSalvos = db.Perfil.AsNoTracking().Where(p => p.idUsuario == idUsuario).ToList();

                var resultado = new List<PerfilAlimentarDTO>();

                foreach (var ing in todosIngredientes)
                {
                    // Procura se o usuário já marcou esse ingrediente
                    var perfil = perfisSalvos.FirstOrDefault(p => p.idIngrediente == ing.ID);

                    resultado.Add(new PerfilAlimentarDTO
                    {
                        IdIngrediente = ing.ID,
                        NomeIngrediente = ing.nome,
                        Status = perfil != null ? perfil.status : null // true, false ou null
                    });
                }

                return resultado;
            }
        }

        // 2. Salva limpando o antigo e inserindo as novas escolhas
        public void SalvarPerfilAlimentar(int idUsuario, List<PerfilAlimentarDTO> listaperfil)
        {
            using (var db = new dbLivroReceita())
            {
                // Limpa o perfil antigo do usuário no banco
                var antigos = db.Perfil.Where(p => p.idUsuario == idUsuario).ToList();
                db.Perfil.RemoveRange(antigos);

                // Insere apenas quem tem Status marcado (true ou false)
                foreach (var item in listaperfil)
                {
                    if (item.Status.HasValue)
                    {
                        db.Perfil.Add(new Perfil
                        {
                            idUsuario = idUsuario,
                            idIngrediente = item.IdIngrediente,
                            status = item.Status.Value
                        });
                    }
                }

                db.SaveChanges();
            }
        }
    }
}