using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Pages.Tarefa.TelasMenuTarefa
{
    public class Todas : PageModel
    {
        private readonly ILogger<Todas> _logger;
        private readonly AppDbContext _context;
        public List<TarefaModel> TarModel { get; set; } = new();
        public Todas(AppDbContext context, ILogger<Todas> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try {
                TarModel = await _context.Tarefas!
                    .Include(p => p.Responsavel)
                    .ToListAsync();
            } catch(Exception exp) {
                _logger.LogError(exp, "Error");
            }

            return Page();
        }
    }
}