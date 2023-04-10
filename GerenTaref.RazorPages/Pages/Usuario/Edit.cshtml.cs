using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Usuario
{
    public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public UsuarioModel UserModel { get; set; } = new();
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Usuarios == null) {
                return NotFound();
            }

            var userModel = await _context.Usuarios.FirstOrDefaultAsync(e => e.UsuarioID == id);

            if(userModel == null) {
                return NotFound();
            }

            UserModel = userModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            var userToUpdate = await _context.Usuarios!.FindAsync(id);

            if(userToUpdate == null) {
                return NotFound();
            }

            userToUpdate.NomeCompleto = UserModel.NomeCompleto;
            userToUpdate.Email = UserModel.Email;
            userToUpdate.Senha = UserModel.Senha;

            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Usuario/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}