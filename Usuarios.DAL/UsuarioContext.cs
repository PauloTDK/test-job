using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Usuarios.BL.Entities;

namespace Usuarios.DAL
{

    public class UsuarioContext : DbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; } 
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured)
                return;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Usuarios.API/appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection1");
            builder.UseSqlServer(connectionString);
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //builder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }

    }
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("Id");
            builder.OwnsOne(u => u.Nome, n =>{
                n.Property(o => o.PrimeiroNome).HasColumnName("Nome").IsRequired();
                n.Property(o => o.SobreNome).HasColumnName("Sobrenome").IsRequired();
                n.Ignore(o => o.Notifications);
                n.Ignore(o => o.Invalid);
                n.Ignore(o => o.Valid);
            });
            builder.OwnsOne(u => u.Email, n =>{
                n.Property(o => o.EmailAddress).HasColumnName("Email").IsRequired();
                n.Ignore(o => o.Notifications);
                n.Ignore(o => o.Invalid);
                n.Ignore(o => o.Valid);
            });
            builder.OwnsOne(u => u.DataNascimento, n =>{
                n.Property(o => o.Data).HasColumnName("DataNascimento").IsRequired();
                n.Ignore(o => o.Notifications);
                n.Ignore(o => o.Invalid);
                n.Ignore(o => o.Valid);
            });
            builder.Property(u => u.Escolaridade)
                   .HasColumnName("Escolaridade")
                   .HasConversion<int>()
                   .IsRequired();
            
            builder.Ignore(o => o.Notifications);
            builder.Ignore(o => o.Invalid);
            builder.Ignore(o => o.Valid);

            builder.ToTable("Usuarios");

        }
    }
}
