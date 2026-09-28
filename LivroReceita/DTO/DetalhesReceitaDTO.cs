using System.Collections.Generic;

namespace LivroReceita.DTO
{
    public class IngredienteDetalheDTO
    {
        public string Nome { get; set; }
        public int? QtdGramas { get; set; }
    }

    public class ProcedimentoDetalheDTO
    {
        public int Sequencia { get; set; }
        public string Descricao { get; set; }
    }

    public class ReceitaDetalhesDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdVezesSalvas { get; set; }
        public int TempoPreparo { get; set; }
        public string Autor { get; set; }
        public string Foto { get; set; }
        public List<IngredienteDetalheDTO> Ingredientes { get; set; } = new List<IngredienteDetalheDTO>();
        public List<ProcedimentoDetalheDTO> Procedimentos { get; set; } = new List<ProcedimentoDetalheDTO>();
    }
}
