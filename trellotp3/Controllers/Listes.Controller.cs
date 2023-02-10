using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using trellotp3.Data;
using trellotp3.Models;

namespace trellotp3.Controllers
{
    public class ListesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private UserManager<IdentityUser> _userManager;
        private string IdProjet;
        public ListesController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
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

        public IActionResult Index(string Id)
        {
            
                IdProjet = Id;
                var projet = _applicationDbContext.Projets.Find(Id);


                var listes = _applicationDbContext.Listes.Where(p => p.IdProjet.Equals(projet.Id));

            if (listes != null)
            {
                return View(listes);
            }else 
                return View("Index",new {Id});
     
        }
        public IActionResult Create(string Id)
        {
            var project = _applicationDbContext.Projets.Find(Id);
            return View("Create", project);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Nom, string Id)
        {
            var projet = _applicationDbContext.Projets.Find(Id);
              Liste liste = new Liste(Nom, projet);
            _applicationDbContext.Listes.Add(liste);
            _applicationDbContext.SaveChanges();
            
            return RedirectToAction("Index", new { Id });
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _applicationDbContext.Listes == null)
            {
                return NotFound();
            }

            var liste = await _applicationDbContext.Listes.FindAsync(id);
            if (liste == null)
            {
                return NotFound();
            }
            return View(liste);
        }

        // POST: Listes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,IdProjet")] Liste liste)
        {
            var idproj = liste.IdProjet;

            if (ModelState.IsValid)
            {
                
                    _applicationDbContext.Update(liste);
                     _applicationDbContext.SaveChanges();
               
                return RedirectToAction("Listes", new { idproj });
            }
            return View(liste);
        }

        // GET: Listes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _applicationDbContext.Listes == null)
            {
                return NotFound();
            }

            var liste = await _applicationDbContext.Listes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (liste == null)
            {
                return NotFound();
            }

            return View(liste);
        }

        // POST: Listes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_applicationDbContext.Listes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Listes'  is null.");
            }
            var liste = await _applicationDbContext.Listes.FindAsync(id);
            if (liste != null)
            {
                _applicationDbContext.Listes.Remove(liste);
            }

            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
    }
