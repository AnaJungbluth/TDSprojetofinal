namespace GerenTaref.RazorPages.Pages.Model
{
    public class TarefaModel
    {
        public int? TarefaID { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string? EstadoTarefa { get; set; }
        public ProjetoModel? Projeto { get; set; }
        public UsuarioModel? Responsavel { get; set; }
    }
}