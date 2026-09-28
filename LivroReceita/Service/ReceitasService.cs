using LivroReceita.Data;
using LivroReceita.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LivroReceita.Service
{
    public class ReceitasService
    {

        public enum TipoOrdenacao
        {
            Gosto,
            TempoPreparo,
            QtdIngredientes
        }

        public List<ReceitaDTO> ListarTodosReceitas(
            int idusuarioLogado,
            string filtro = "",
            TipoOrdenacao ordenacao = TipoOrdenacao.Gosto,
            bool isFavorita = false
            )
        {
            using (var db = new dbLivroReceita())
            {
                var idsReceitasFavoritadas = db.ReceitaFavorira
                                               .AsNoTracking()
                                               .Where(r => r.UsuarioID == idusuarioLogado && r.saved == 1)
                                               .Select(rf => rf.ReceitaID)
                                               .ToList();


                var perfilUsuario = db.Perfil
                                      .AsNoTracking()
                                      .Where(p => p.idPerfil == idusuarioLogado || p.idUsuario == idusuarioLogado)
                                      .ToList();

                // query base para conseguir fazer as buscas por nome, classificação e ingredientes
                var query = db.UsuarioReceita
                              .Include(r => r.NotaReceita)
                              .Include(r => r.IngredientesReceita)
                                .ThenInclude(ir => ir.Ingrediente)
                              .AsNoTracking();


                if (isFavorita) query = query.Where(r => idsReceitasFavoritadas.Contains(r.id));

                // Se o filtro não for null nem um espaço em branco
                if (!string.IsNullOrWhiteSpace(filtro))
                {

                    // Transformamos o filtro em número se der
                    bool ehNumero = int.TryParse(filtro, out int notaBuscada);

                    // pegamos a query base que tem acesso a tabela de UsuarioReceita, NotaReceita, IngredientesRecaita e Ingredientes
                    // fazemos as buscas onde o filtro se encaixar
                    query = query.Where(r =>

                    // Se o nome da receitar conter o que está sendo digitado, vai retornar a receita por nome
                        r.NomeReceita.ToLower().Contains(filtro) ||

                        // Se o ingrediente da receita conter o que está sendo digitado, vai retornar por ingrediente
                        r.IngredientesReceita.Any(ir => ir.Ingrediente.nome.ToLower().Contains(filtro)) ||

                        // Se o ehNumero for true e tiver uma receita com a nota que foi digitada, ex:7, vai retonar por nota buscada
                        (ehNumero && r.NotaReceita.Any(n => n.nota == notaBuscada))
                    );

                }

                var receitas = query.ToList();

                IEnumerable<UsuarioReceita> receitasOrdenadas;

                switch (ordenacao)
                {
                    case TipoOrdenacao.Gosto:
                        receitasOrdenadas = receitas.OrderByDescending(r => CalcularGosto(r, perfilUsuario));
                        break;

                    case TipoOrdenacao.TempoPreparo:
                        receitasOrdenadas = receitas.OrderBy(r => r.TempoMinutos ?? int.MaxValue);
                        break;

                    case TipoOrdenacao.QtdIngredientes:
                        receitasOrdenadas = receitas.OrderBy(r => r.IngredientesReceita.Count);
                        break;

                    default:
                        receitasOrdenadas = receitas;
                        break;
                }


                return receitasOrdenadas.Select(r => new ReceitaDTO
                {
                    ID = r.id,
                    Nome = r.NomeReceita,
                    TempoPreparo = r.TempoMinutos ?? 0,
                    IsFavorita = idsReceitasFavoritadas.Contains(r.id)
                }).ToList();
            }
        }

        private int CalcularGosto(UsuarioReceita receita, List<Perfil> perfis)
        {
            int score = 0;
            foreach (var ingrediente in receita.IngredientesReceita)
            {
                var perfilItem = perfis.FirstOrDefault(p => p.idIngrediente == ingrediente.IngredienteID);
                if (perfilItem != null)
                {
                    if (perfilItem.status == true) score++;
                    if (perfilItem.status == false) score--;
                }
            }

            return score;
        }


        public void AlternarReceitaFavorita(int usuarioId, int receitaId, bool isFavorita)
        {
            using (var db = new dbLivroReceita())
            {
                var favorito = db.ReceitaFavorira
                                 .FirstOrDefault(fr => fr.UsuarioID == usuarioId
                                                    && fr.ReceitaID == receitaId);

                if (favorito != null)
                {
                    favorito.saved = isFavorita ? 1 : (int?)null;
                }
                else if (isFavorita)
                {
                    db.ReceitaFavorira.Add(new ReceitaFavorira
                    {
                        UsuarioID = usuarioId,
                        ReceitaID = receitaId,
                        saved = 1
                    });
                }

                db.SaveChanges();
            }
        }

        public ReceitaDetalhesDTO DetalhesReceita(int receitaId)
        {
            using (var db = new dbLivroReceita())
            {
                var receita = db.UsuarioReceita
                                .Include(r => r.Usuario)
                                .Include(r => r.IngredientesReceita.Select(ir => ir.Ingrediente))
                                .Include(r => r.ProcedimentoReceitas)
                                .AsNoTracking()
                                .FirstOrDefault(r => r.id == receitaId);


                if (receita == null) return null;

                var qtdSalvos = db.ReceitaFavorira
                                  .AsNoTracking()
                                  .Count(f => f.ReceitaID == receitaId && f.saved == 1);

                var dto = new ReceitaDetalhesDTO
                {
                    Id = receitaId,
                    Nome = receita.NomeReceita,
                    TempoPreparo = receita.TempoMinutos ?? 0,
                    Autor = receita.Usuario.nome,
                    QtdVezesSalvas = qtdSalvos
                };

                if (receita.IngredientesReceita != null)
                {
                    foreach (var ingrediente in receita.IngredientesReceita)
                    {
                        if (ingrediente.Ingrediente != null)
                        {
                            dto.Ingredientes.Add(new IngredienteDetalheDTO
                            {
                                Nome = ingrediente.Ingrediente.nome,
                                QtdGramas = ingrediente.qtdGramas
                            });
                        }
                    }
                }


                if (receita.ProcedimentoReceitas != null)
                {
                    var etapas = receita.ProcedimentoReceitas
                        .OrderBy(p => p.stepSequence)
                        .ToList();

                    foreach (var p in etapas)
                    {
                        if (!string.IsNullOrWhiteSpace(p.step))
                        {
                            dto.Procedimentos.Add(new ProcedimentoDetalheDTO
                            {
                                Sequencia = p.stepSequence ?? 0,
                                Descricao = p.step
                            });
                        }
                    }
                }

                return dto;
            }
        }

        public ReceitaDTO ObterReceitaAleatoriaNaoSalva(int idUsuario)
        {
            using (var db = new dbLivroReceita())
            {
                // 1. Busca os IDs das receitas favoritadas/salvas do usuário
                var idsFavoritadas = db.ReceitaFavorira
                                       .AsNoTracking()
                                       .Where(r => r.UsuarioID == idUsuario && r.saved == 1)
                                       .Select(rf => rf.ReceitaID)
                                       .ToList();

                // 2. Filtra no banco apenas as receitas que NÃO foram salvas
                var receitasNaoSalvas = db.UsuarioReceita
                                          .AsNoTracking()
                                          .Where(r => !idsFavoritadas.Contains(r.id))
                                          .Select(r => new ReceitaDTO
                                          {
                                              ID = r.id,
                                              Nome = r.NomeReceita,
                                              TempoPreparo = r.TempoMinutos ?? 0,
                                              IsFavorita = false
                                          })
                                          .ToList();

                if (receitasNaoSalvas.Count == 0) return null;

                // 3. Retorna um item aleatório
                Random random = new Random();
                return receitasNaoSalvas[random.Next(receitasNaoSalvas.Count)];
            }
        }
    }
}
