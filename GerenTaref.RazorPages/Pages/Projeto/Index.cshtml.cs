using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Projeto
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public List<ProjetoModel> ProjetoList { get; set; } = new();
        public UsuarioModel? UserModel { get; set; } = new();
        private int? IdUser { get; set; }
        public Index(AppDbContext context)
        {
           _context = context;
        }

        public async Task<IActionResult> OnGetAsync(){
            ProjetoList = await _context.Projetos!.Include(p => p.Responsavel).ToListAsync();
            return Page();
        }
    }
}