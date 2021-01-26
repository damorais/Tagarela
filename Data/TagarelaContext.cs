using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tagarela.Models;

namespace Tagarela.Data
{
    public class TagarelaContext : IdentityDbContext<IdentityUser>
    {
        public TagarelaContext(DbContextOptions<TagarelaContext> options)
            : base(options)
        {
        }

        public DbSet<Mensagem> Mensagem { get; set; }
    }
}