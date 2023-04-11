using Microsoft.AspNetCore.Mvc.RazorPages;
using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Projeto
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ProjetoModel ProjetoModel { get; set; } = new();
        [BindProperty]
        public int? IdUser { get; set; }
        public List<UsuarioModel>? UserModel { get; set; }
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            ProjetoModel.Responsavel = await _context.Usuarios!.FindAsync(IdUser);
            
            try {
                _context.Add(ProjetoModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projeto/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync() {
            UserModel = await _context.Usuarios!.ToListAsync();

            if (UserModel.Count == 0) {
                TempData["ErroUser"] = "Não há usuários cadastrados!"; // define a mensagem na TempData
                return RedirectToPage("/Projeto/Index"); // redireciona para a página de índice
            }

            return Page();
        }
    }
}