using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using trellotp3.Data;
using trellotp3.Models;

namespace trellotp3.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private UserManager<IdentityUser> _userManager;
        public ProjectController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public Utilisateur getUserConnect()
        {
            var userMail = User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.Users.Single(p => p.Email == userMail);
            var utilisateur = _applicationDbContext.Utilisateurs.Single(p => p.Id == user.Id);
            return utilisateur;
        }

        public IActionResult Index()
        {
            var userMail = User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.Users.Single(p => p.Email == userMail);
            var utilisateur = _applicationDbContext.Utilisateurs.Single(p => p.Id == user.Id);
            var listeProjet = _applicationDbContext.Projets.Where(p => p.UtilisateurProjets.Any(s => s.IdUtilisateur == utilisateur.Id));
            return View(listeProjet);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Nom, string DescriptionPro)
        {
            var utilisateur = getUserConnect();
            var id = _applicationDbContext.Projets.Max(p => p.Id);
            int id1 = Int32.Parse(id);
            id1 += 1;
            Projet projet = new Projet(id1.ToString(),Nom, DescriptionPro);
            _applicationDbContext.Projets.Add(projet);
            _applicationDbContext.SaveChanges();
           
            UtilisateurProjet utilisateurProjet = new(utilisateur, projet);
            _applicationDbContext.Add<UtilisateurProjet>(utilisateurProjet);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");




        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _applicationDbContext.Projets == null)
            {
                return NotFound();
            }

            var projet = await _applicationDbContext.Projets
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (projet == null)
            {
                return NotFound();
            }

            return View(projet);
        }

        // POST: Projets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_applicationDbContext.Projets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projets'  is null.");
            }
            var projet =  _applicationDbContext.Projets.Find(id);
            var utilisateurproj = _applicationDbContext.UtilisateurProjet.Single(p=>p.IdProjet.Equals(projet.Id));
            var liste = _applicationDbContext.Listes.Where(p => p.IdProjet.Equals(projet.Id));
          
            foreach(var item in liste)
            {
                var carte = _applicationDbContext.Cartes.Where(p => p.IdListe.Equals(item.Id));
                foreach(var carte2 in carte)
                {
                _applicationDbContext.Cartes.Remove(carte2);
                _applicationDbContext.SaveChanges();

                }
                _applicationDbContext.Listes.Remove(item);
                _applicationDbContext.SaveChanges();
            }
            
            if (projet != null)
            {
                _applicationDbContext.Remove(utilisateurproj);
                _applicationDbContext.SaveChanges();
                _applicationDbContext.Projets.Remove(projet);
            }

            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _applicationDbContext.Projets == null)
            {
                return NotFound();
            }

            var projet =  _applicationDbContext.Projets.Find(id);
            if (projet == null)
            {
                return NotFound();
            }
            return View(projet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,DescriptionPro,DateCreation,IdUtilisateur")] Projet projet)
        {
           

            if (ModelState.IsValid)
            {
               
                    _applicationDbContext.Update(projet);
                    await _applicationDbContext.SaveChangesAsync();
                
               
                   
                
                return RedirectToAction(nameof(Index));
            }
            return View(projet);
        }
    }
}
