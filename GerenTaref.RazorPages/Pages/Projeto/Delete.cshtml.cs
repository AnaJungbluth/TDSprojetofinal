using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Projeto
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ProjetoModel ProjetoModel { get; set; } = new();
        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Projetos == null) {
                return NotFound();
            }

            var projetoModel = await _context.Projetos.FirstOrDefaultAsync(e => e.ProjetoID == id);

            if(projetoModel == null) {
                return NotFound();
            }

            ProjetoModel = projetoModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var projetoToDelete = await _context.Projetos!.FindAsync(id);

            if(projetoToDelete == null) {
                return NotFound();
            }

            try {
                _context.Projetos.Remove(projetoToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projeto/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}