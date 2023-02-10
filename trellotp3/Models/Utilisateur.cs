using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace trellotp3.Models;

public partial class Utilisateur 
{
   
    public string Id { get; set; }
    public string Nom { get; set; } = null!;

    public string? Prenom { get; set; }
    public int? idUtilisateurProjets { get; set; }
   

    public DateTime DateInscription { get; set; }

    public virtual ICollection<Commentaire>? Commentaires { get; } = new List<Commentaire>();

    public virtual ICollection<Projet>? Projets { get; } = new List<Projet>();

    public virtual ICollection<UtilisateurProjet>? UtilisateurProjets { get; } = new List<UtilisateurProjet>();

    public Utilisateur(string id, string nom, string prenom)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prenom = prenom;
        this.DateInscription = DateTime.Now;
    }
    public Utilisateur() { }    

}
