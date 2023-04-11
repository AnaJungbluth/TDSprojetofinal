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
        [BindProperty]
        public int? IdUser { get; set; }
        [BindProperty]
        public int? IdProjeto { get; set; }
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return Page();

            if(IdUser == null || IdProjeto == null)
                return NotFound();
            
            TarModel.Responsavel = await _context.Usuarios!.FindAsync(IdUser);
            TarModel.Projeto = await _context.Projetos!.FindAsync(IdProjeto);

            try {
                _context.Add(TarModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Tarefa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync() {
            try{
                UserModel = await _context.Usuarios!.ToListAsync();
            } catch(DbUpdateException) {
                return Page();
            }
            
            try{
                ProjModel = await _context.Projetos!.ToListAsync();
            } catch(DbUpdateException) {
                return Page();
            }
 
            return Page();
        }
    }
}