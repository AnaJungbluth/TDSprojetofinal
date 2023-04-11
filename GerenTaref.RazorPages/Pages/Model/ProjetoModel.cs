using System.ComponentModel.DataAnnotations;

namespace GerenTaref.RazorPages.Pages.Model
{
    public class ProjetoModel
    {
    
        public int? ProjetoID { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public UsuarioModel? Responsavel { get; set; }
    }
}