using System;
using System.Collections.Generic;

namespace trellotp3.Models;

public partial class Carte
{
    public int Id { get; set; }

    public string? Titre { get; set; }

    public string? Description { get; set; }

    public DateTime? DateCreation { get; set; }

    public int? IdListe { get; set; }

    public virtual ICollection<Commentaire>? Commentaires { get; } = new List<Commentaire>();

    public virtual ICollection<Etiquette>? Etiquettes { get; } = new List<Etiquette>();

    public virtual Liste? IdListeNavigation { get; set; } = null!;
    public Carte(string Titre, string Description, Liste liste) {
        this.Titre = Titre;
        this.Description = Description;
        this.IdListeNavigation= liste;
        this.IdListe = liste.Id;
        this.DateCreation= DateTime.Now;
    
    
    }
    public Carte() { }

}
