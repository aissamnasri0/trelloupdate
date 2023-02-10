using System;
using System.Collections.Generic;

namespace trellotp3.Models;

public partial class Liste
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? IdProjet { get; set; }

    public virtual ICollection<Carte>? Cartes { get; } = new List<Carte>();

    public virtual Projet? IdProjetNavigation { get; set; }
    public Liste( string nom, Projet ProjetNavigation)
    {
        
        Nom = nom;
        IdProjetNavigation = ProjetNavigation;
        this.IdProjet = ProjetNavigation.Id;
    }
    public Liste() { }
}
