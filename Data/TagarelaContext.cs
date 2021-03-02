using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tagarela.Models;

namespace Tagarela.Data
{
    public class TagarelaContext : IdentityUserContext<User, Guid>
    {
        public TagarelaContext(DbContextOptions<TagarelaContext> options)
            : base(options)
        {
        }

        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Segue)
                .WithMany(u => u.SeguidoPor)
                .UsingEntity(t => t.ToTable("SeguidoSeguePor"));
        }
    }
}