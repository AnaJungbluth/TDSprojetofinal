using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenTaref.RazorPages.Data;
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerenTaref.RazorPages.Pages.Usuario
{
    public class LoginUsuario : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public UsuarioModel Login { get; set; }

        public LoginUsuario(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            // Lógica para processar o GET da página de login (se necessário)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == Login.Email);

                if (usuario != null && usuario.Senha == Login.Senha)
                {
                    // Login bem-sucedido
                    return RedirectToPage("../Tarefa/Index");
                }

                // Login inválido
                ModelState.AddModelError(string.Empty, "Credenciais inválidas.");
            }

            // Algo deu errado, retorna a página de login para exibir os erros
            return Page();
        }
    }
}