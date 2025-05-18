using Dapper;
using Microsoft.AspNetCore.Mvc;
using Projet_salle_de_gym.Core.Infrastructure;
using Projet_salle_de_gym.Core.Services;
using Projet_salle_de_gym.Models.Panier;
using Projet_salle_de_gym.Models.Products;

namespace Projet_salle_de_gym.Controllers
{
    public class PanierController : Controller
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public PanierController(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Ajouter(int id)
        {
            using var connection = await _connectionProvider.CreateConnection();

            var produit = await connection.QueryFirstOrDefaultAsync<Produit>(
                "SELECT * FROM produit WHERE id_prod = @id", new { id });

            if (produit != null)
            {
                var item = new PanierItem
                {
                    IdProduit = produit.Id_prod,
                    Nom = produit.Nom_produit,
                    Prix = produit.Prix,
                    Quantite = 1
                };

                PanierService.AjouterProduit(HttpContext.Session, item);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            var panier = PanierService.GetPanier(HttpContext.Session);
            return View(panier);
        }

        [HttpPost]
        public IActionResult Supprimer(int id)
        {
            PanierService.SupprimerProduit(HttpContext.Session, id);
            return RedirectToAction("Index");
        }

    }
}