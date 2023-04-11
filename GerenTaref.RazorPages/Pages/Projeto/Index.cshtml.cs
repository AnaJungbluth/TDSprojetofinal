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
        public List<ProjetoModel> ProjetoList { get; set; } = new();
        public Index(AppDbContext context)
        {
           _context = context;
        }

        public async Task<IActionResult> OnGetAsync(){
            ProjetoList = await _context.Projetos!.ToListAsync();
            return Page();
        }
    }
}