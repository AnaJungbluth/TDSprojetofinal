using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Usuario
{
    public class Delete : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public UsuarioModel UserModel { get; set; } = new();
        public Delete(AppDbContext context)
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
            var userToDelete = await _context.Usuarios!.FindAsync(id);

            if(userToDelete == null) {
                return NotFound();
            }

            try {
                _context.Usuarios.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Usuario/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}