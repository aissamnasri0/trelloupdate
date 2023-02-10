using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using trellotp3.Data;
using trellotp3.Models;

namespace trellotp3.Controllers
{
    public class CartesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private UserManager<IdentityUser> _userManager;
        public static int idListe;
        public CartesController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public IActionResult Index(int Id)
        {
            var liste = _applicationDbContext.Listes.Find(Id);
            var carte= _applicationDbContext.Cartes.Where(p=> p.IdListe.Equals(liste.Id));
            return View(carte);
        }
        public IActionResult Create(int IdListe)
        {
            idListe= IdListe;
           
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Titre, string Description,int Id)
        {
            var liste = _applicationDbContext.Listes.Find(Id);
           Carte carte = new Carte(Titre,Description,liste);
            _applicationDbContext.Cartes.Add(carte);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index",new { Id });
        }
    }
}
