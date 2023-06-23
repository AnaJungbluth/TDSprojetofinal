using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Tarefa
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public TarefaModel TarModel { get; set; } = new();
        public Delete(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Usuarios == null) {
                return NotFound();
            }

            var tarModel = await _context.Tarefas!
                .Include(p => p.Responsavel)
                .FirstOrDefaultAsync(e => e.TarefaID == id);

            if(tarModel == null) {
                return NotFound();
            }

            TarModel = tarModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var tarefaToDelete = await _context.Tarefas!.FindAsync(id);

            if(tarefaToDelete == null) {
                return NotFound();
            }

            try {
                _context.Tarefas.Remove(tarefaToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Tarefa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}