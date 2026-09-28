namespace LivroReceita.DTO
{
    public class ReceitaDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool IsFavorita { get; set; }
        public int? TempoPreparo { get; set; }
    }
}
