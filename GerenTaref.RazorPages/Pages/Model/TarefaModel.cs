namespace GerenTaref.RazorPages.Pages.Model
{
    public class TarefaModel
    {
        public int? TarefaID { get; set; }
        public string? Nome { get; set; }
        public string? Nota { get; set; }
        public DateTime? DataFinal { get; set; }
        public string? EstadoTarefa { get; set; }
        public UsuarioModel? Responsavel { get; set; }
    }
}