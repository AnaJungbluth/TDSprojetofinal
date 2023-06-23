using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Tarefa
{
public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public TarefaModel TarModel { get; set; } = new();
        [BindProperty]
        public int? IdUser { get; set; }
        public List<UsuarioModel>? UserModel { get; set; }
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var tarefaModel = await _context.Tarefas!
                .Include(p => p.Responsavel)
                .FirstOrDefaultAsync(e => e.TarefaID == id);

            UserModel = await _context.Usuarios!.ToListAsync();

            if(tarefaModel == null) {
                return NotFound();
            }

            IdUser = tarefaModel.Responsavel!.UsuarioID;

            TarModel = tarefaModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            var tarefaToUpdate = await _context.Tarefas!.FindAsync(id);

            if(tarefaToUpdate == null) {
                return NotFound();
            }

            tarefaToUpdate.Nome = TarModel.Nome;
            tarefaToUpdate.Nota = TarModel.Nota;
            tarefaToUpdate.DataFinal = TarModel.DataFinal;
            tarefaToUpdate.EstadoTarefa = TarModel.EstadoTarefa;
            tarefaToUpdate.Responsavel = await _context.Usuarios!.FindAsync(IdUser);
            
            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Tarefa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}