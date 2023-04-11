using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Tarefa
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public TarefaModel TarModel { get; set; } = new();
        public List<UsuarioModel>? UserModel { get; set; }
        public List<ProjetoModel>? ProjModel { get; set; }
        public int idUser { get; set; }
        public int idProjeto { get; set; }
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();
            
            try {
                _context.Add(TarModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Tarefa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync() {
            UserModel = await _context.Usuarios!.ToListAsync();
            try{
                ProjModel = await _context.Projetos!.ToListAsync();
            } catch(Exception exp) {
                
            }
 
            return Page();
        }
    }
}