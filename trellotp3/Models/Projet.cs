using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace trellotp3.Models;

public partial class Projet
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? DescriptionPro { get; set; }

    public DateTime DateCreation { get; set; }

    public string? IdUtilisateur { get; set; }

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; } = null!;

    public virtual ICollection<Liste>? Listes { get; } = new List<Liste>();

    public virtual ICollection<UtilisateurProjet>? UtilisateurProjets { get; } = new List<UtilisateurProjet>();
    
    public Projet(string Id,string nom, string Description)
    {
        this.Id = Id;
       
        this.Nom = nom;
        this.DescriptionPro = Description;

        DateCreation = DateTime.Now;
        

    }
    public Projet() {
           
    }
}
