using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Usuario
{
    public class Index : PageModel
    {
        private readonly AppDbContext _context;
        public List<UsuarioModel> UserModel { get; set; } = new();
        
        public Index(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            UserModel = await _context.Usuarios!.ToListAsync();
            return Page();
        }
    }
}