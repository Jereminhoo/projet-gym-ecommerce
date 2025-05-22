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
        [HttpPost]
        public async Task<IActionResult> Acheter()
        {

            int? utilisateurId = HttpContext.Session.GetInt32("Id_user");


            // 1. Récupérer le panier depuis la session
            var panier = PanierService.GetPanier(HttpContext.Session);
            if (!panier.Any())
            {
                TempData["Message"] = "Votre panier est vide.";
                return RedirectToAction("Index");
            }

            // 2. Connexion à la base
            using var connection = await _connectionProvider.CreateConnection();

            // 3. Créer une commande
            var commandeId = await connection.ExecuteScalarAsync<int>(
                @"INSERT INTO commande (date_com, id_util)
                VALUES (@DateCommande, @UtilisateurId)
                RETURNING id_com;",
                new
                {
                    DateCommande = DateTime.Now,
                    UtilisateurId = utilisateurId
                });

            // 4. Ajouter les lignes dans la table detail
            foreach (var item in panier)
            {
                await connection.ExecuteAsync(
                    @"INSERT INTO detail (id_com, quantite, total_prix_unit)
              VALUES (@CommandeId, @Quantite, @Total);",
                    new
                    {
                        CommandeId = commandeId,
                        Quantite = item.Quantite,
                        Total = item.Prix * item.Quantite
                    });
            }

            // 5. Vider le panier
            PanierService.SavePanier(HttpContext.Session, new List<PanierItem>());

            // 6. Message de confirmation
            TempData["Message"] = $"Commande #{commandeId} créée avec succès.";
            return RedirectToAction("Index");
        }
    }
}