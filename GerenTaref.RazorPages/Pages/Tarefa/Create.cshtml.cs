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

        [BindProperty]
        public int? IdUser { get; set; }
        
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return Page();
            
            TarModel.Responsavel = await _context.Usuarios!.FindAsync(IdUser);

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
            
            if (UserModel.Count == 0) {
                TempData["ErroUser"] = "Não há usuários cadastrados!"; // define a mensagem na TempData
                return RedirectToPage("/Tarefa/Index"); // redireciona para a página de índice
            }
            
            return Page();
        }
    }
}