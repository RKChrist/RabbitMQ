using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rab;

namespace WebApplication3.Data
{
    public class WebApplication3Context : DbContext
    {
        public WebApplication3Context (DbContextOptions<WebApplication3Context> options)
            : base(options)
        {
        }

        public DbSet<Rab.Tour> Tour { get; set; } = default!;
    }
}
