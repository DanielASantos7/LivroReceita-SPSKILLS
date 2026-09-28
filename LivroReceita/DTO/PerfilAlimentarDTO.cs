namespace LivroReceita.DTO
{
    public enum TipoPreferencia
    {
        Indiferente = 0,
        Apreciado = 1,
        Evitado = 2,
    }

    public class PerfilAlimentarDTO
    {
        public string NomeIngrediente { get; set; }
        public string FotoIngrediente { get; set; }
        public int IdIngrediente { get; set; }
        public TipoPreferencia Preferencia { get; set; }
        public bool? Status { get; set; }// true = Apreciado, false = evitado, null = Indiferente
    }
}
