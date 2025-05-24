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

            var panier = PanierService.GetPanier(HttpContext.Session);
            if (!panier.Any())
            {
                TempData["Message"] = "Votre panier est vide.";
                return RedirectToAction("Index");
            }

            using var connection = await _connectionProvider.CreateConnection();

            foreach (var item in panier)
            {
                var stock = await connection.ExecuteScalarAsync<int>(
                    "SELECT stock FROM produit WHERE id_prod = @Id", new { Id = item.IdProduit });

                if (stock < item.Quantite)
                {
                    TempData["Message"] = $"Stock insuffisant pour \"{item.Nom}\" (stock actuel : {stock}).";
                    return RedirectToAction("Index");
                }
            }

            var commandeId = await connection.ExecuteScalarAsync<int>(
                @"INSERT INTO commande (date_com, id_util)
          VALUES (@DateCommande, @UtilisateurId)
          RETURNING id_com;",
                new
                {
                    DateCommande = DateTime.Now,
                    UtilisateurId = utilisateurId
                });

            foreach (var item in panier)
            {
                // Insère la ligne dans la table des détails
                await connection.ExecuteAsync(
                    @"INSERT INTO detail (id_com, id_prod, quantite, total_prix_unit)
              VALUES (@CommandeId, @IdProduit, @Quantite, @Total);",
                    new
                    {
                        CommandeId = commandeId,
                        IdProduit = item.IdProduit,
                        Quantite = item.Quantite,
                        Total = item.Prix * item.Quantite
                    });

                // Diminue le stock du produit
                await connection.ExecuteAsync(
                    @"UPDATE produit
              SET stock = stock - @Quantite
              WHERE id_prod = @IdProduit;",
                    new
                    {
                        IdProduit = item.IdProduit,
                        Quantite = item.Quantite
                    });
            }

            PanierService.SavePanier(HttpContext.Session, new List<PanierItem>());

            TempData["Message"] = $"Commande #{commandeId} créée avec succès.";
            return RedirectToAction("Index");
        }





        [HttpGet]
        public async Task<IActionResult> MesCommandes()
        {
            var utilisateurId = HttpContext.Session.GetInt32("Id_user");

            using var connection = await _connectionProvider.CreateConnection();

            var commandes = await connection.QueryAsync<PanierItem>(
         @"SELECT 
            c.id_com AS IdCommande,
            c.date_com AS DateCommande,
            p.nom_produit AS Nom,
            d.quantite AS Quantite,
            d.total_prix_unit AS Prix
          FROM commande c
          JOIN detail d ON c.id_com = d.id_com
          JOIN produit p ON d.id_prod = p.id_prod
          WHERE c.id_util = @Id
          ORDER BY c.date_com DESC",
         new { Id = utilisateurId });



            return View(commandes.ToList());
        }


    }

}