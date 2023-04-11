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
        [BindProperty]
        public int? IdProjeto { get; set; }
        public List<ProjetoModel>? ProjModel { get; set; }
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Projetos == null) {
                return NotFound();
            }

            var tarefaModel = await _context.Tarefas!
                .Include(p => p.Responsavel)
                .Include(k => k.Projeto)
                .FirstOrDefaultAsync(e => e.TarefaID == id);

            UserModel = await _context.Usuarios!.ToListAsync();
            ProjModel = await _context.Projetos!.ToListAsync();

            if(tarefaModel == null) {
                return NotFound();
            }

            IdUser = tarefaModel.Responsavel!.UsuarioID;
            IdProjeto = tarefaModel.Projeto!.ProjetoID;

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

            tarefaToUpdate.DataInicio = TarModel.DataInicio;
            tarefaToUpdate.DataFinal = TarModel.DataFinal;
            tarefaToUpdate.EstadoTarefa = TarModel.EstadoTarefa;
            tarefaToUpdate.Responsavel = await _context.Usuarios!.FindAsync(IdUser);
            tarefaToUpdate.Projeto = await _context.Projetos!.FindAsync(IdProjeto);
            
            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Tarefa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}