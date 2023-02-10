using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using trellotp3.Models;

namespace trellotp3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Carte> Cartes { get; set; }

        public virtual DbSet<Commentaire> Commentaires { get; set; }

        public virtual DbSet<Etiquette> Etiquettes { get; set; }

        public virtual DbSet<Liste> Listes { get; set; }

        public virtual DbSet<Projet> Projets { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<UtilisateurProjet> UtilisateurProjet { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext() { }
    }
}