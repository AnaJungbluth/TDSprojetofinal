using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GerenTaref.RazorPages.Pages
{
    public class Funcionalidade : PageModel
    {
        private readonly ILogger<Funcionalidade> _logger;

        public Funcionalidade(ILogger<Funcionalidade> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}