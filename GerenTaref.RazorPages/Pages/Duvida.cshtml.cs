using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GerenTaref.RazorPages.Pages
{
    public class Duvida : PageModel
    {
        private readonly ILogger<Duvida> _logger;

        public Duvida(ILogger<Duvida> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}