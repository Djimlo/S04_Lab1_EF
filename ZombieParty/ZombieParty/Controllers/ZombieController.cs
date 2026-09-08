using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZombieParty.Models;
using ZombieParty.Models.Models;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class ZombieController : Controller
    {
        private ZombiePartyDbContext _baseDonnees { get; set; }

        public ZombieController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            //this.ViewBag.MaListe = _baseDonnees.Zombies.ToList();
            List<Zombie> zombiesList = _baseDonnees.Zombies.ToList();

            return View(zombiesList);
        }

        //public IActionResult Create()
        //{
        //    //ViewBag.ZombieTypes = new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName", null);
        //    ZombieVM zombieVM = new ZombieVM();
        //    zombieVM.ZombieTypeSelectList = new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName");
        //    return View(zombieVM);
        //}
        public IActionResult Create()
        {
            ZombieVM zombieVM = new ZombieVM();

            zombieVM.Zombie = new Zombie();

            zombieVM.ZombieTypeSelectList =  new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName");

            return View(zombieVM);
        }


        [HttpPost]
        public IActionResult Create(/*Zombie zombie*/ ZombieVM zombieVM)
        {
            //Si le modèle est valide le zombie est ajouté et nous sommes redirigé vers index.
            if (ModelState.IsValid)
            {
                _baseDonnees.Zombies.Add(/*zombie*/ zombieVM.Zombie);
                _baseDonnees.SaveChanges();
                TempData["Success"] = $"Zombie {/*zombie.Name*/ zombieVM.Zombie.Name} added";
                return this.RedirectToAction("Index");
            }
            //Il faut repopuler le zombieType dans le ViewBag
            //Aller chercher le ZombieType sélectionné, rappel 2W5 Linq
            //Ce que j'ai commenté ici (les 03 lignes ci-dessous)
            //ZombieType selectedZombieType = _baseDonnees.ZombieTypes.Where(zt => zt.Id == zombie.ZombieTypeId).SingleOrDefault();
            //zombie.ZombieType = selectedZombieType;
            //ViewBag.ZombieTypes = new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName", selectedZombieType);
            zombieVM.ZombieTypeSelectList = new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName");

            return View(/*zombie*/ zombieVM); // retourne l'objet pour avoir les données
        }

    }
}
