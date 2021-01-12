using Microsoft.EntityFrameworkCore;
using Tagarela.Models;

namespace Tagarela.Data
{
    public class TagarelaContext : DbContext
    {
        public TagarelaContext(DbContextOptions<TagarelaContext> options)
            : base(options)
        {
        }

        public DbSet<Mensagem> Mensagem { get; set; }
    }
}