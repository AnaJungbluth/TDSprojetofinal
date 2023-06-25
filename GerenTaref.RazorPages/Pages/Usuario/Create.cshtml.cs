using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Usuario
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public UsuarioModel UserModel { get; set; } = new();
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();
            
            try {
                _context.Add(UserModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("../Tarefa/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}
