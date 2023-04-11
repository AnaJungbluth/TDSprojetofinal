using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Projeto
{
public class Edit : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ProjetoModel ProjetoModel { get; set; } = new();
        [BindProperty]
        public int? IdUser { get; set; }
        public List<UsuarioModel>? UserModel { get; set; }
        public Edit(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _context.Projetos == null) {
                return NotFound();
            }

            var projetoModel = await _context.Projetos.Include(p => p.Responsavel).FirstOrDefaultAsync(e => e.ProjetoID == id);
            UserModel = await _context.Usuarios!.ToListAsync();

            if(projetoModel == null) {
                return NotFound();
            }

            IdUser = projetoModel.Responsavel!.UsuarioID;
            ProjetoModel = projetoModel;

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            var projetoToUpdate = await _context.Projetos!.FindAsync(id);

            if(projetoToUpdate == null) {
                return NotFound();
            }

            projetoToUpdate.Nome = ProjetoModel.Nome;
            projetoToUpdate.Descricao = ProjetoModel.Descricao;
            projetoToUpdate.Responsavel = await _context.Usuarios!.FindAsync(IdUser);

            try {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projeto/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}