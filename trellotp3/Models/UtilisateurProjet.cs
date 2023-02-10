using System;
using System.Collections.Generic;

namespace trellotp3.Models;

public partial class UtilisateurProjet
{
    public int Id { get; set; }

    public string? IdUtilisateur { get; set; }

    public string? IdProjet { get; set; }

    public virtual Projet? IdProjetNavigation { get; set; } = null!;

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; } = null!;
    public UtilisateurProjet(Utilisateur utilisateur, Projet projet)
    {
        this.IdUtilisateurNavigation = utilisateur;
        this.IdUtilisateur = utilisateur.Id;
        this.IdProjetNavigation = projet;
        this.IdProjet = projet.Id;
    }
    public UtilisateurProjet() { }  
}
