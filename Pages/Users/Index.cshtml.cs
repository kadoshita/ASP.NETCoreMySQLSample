using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreMySQLSample.Data;
using ASP.NETCoreMySQLSample.Models;

namespace ASP.NETCoreMySQLSample.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ASP.NETCoreMySQLSample.Data.UserContext _context;

        public IndexModel(ASP.NETCoreMySQLSample.Data.UserContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
