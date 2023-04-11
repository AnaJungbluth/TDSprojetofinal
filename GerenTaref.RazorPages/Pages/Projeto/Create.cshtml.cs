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
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();
            
            try {
                _context.Add(ProjetoModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projeto/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}