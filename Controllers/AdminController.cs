using Dapper;
using Microsoft.AspNetCore.Mvc;
using Projet_salle_de_gym.Core.Infrastructure;
using Projet_salle_de_gym.Models.Utilisateurs;
using Projet_salle_de_gym.Models.Products;

namespace Projet_salle_de_gym.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public AdminController(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        // GESTION UTILISATEURS
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using var connection = await _connectionProvider.CreateConnection();
            var utilisateurs = (await connection.QueryAsync<Utilisateur>(
                "SELECT * FROM utilisateur WHERE admin = false"));
            return View(utilisateurs.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Supprimer(int id)
        {
            using var connection = await _connectionProvider.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM utilisateur WHERE id_util = @id", new { id });
            return RedirectToAction("Index");
        }

        // GESTION PRODUITS (ARTICLES)
        [HttpGet]
        public async Task<IActionResult> GererArticles()
        {
            using var connection = await _connectionProvider.CreateConnection();
            var articles = await connection.QueryAsync<Produit>("SELECT * FROM produit");
            return View(articles.ToList());
        }

        [HttpGet]
        public IActionResult CreerArticle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreerArticle(Produit produit, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(imageFile.FileName); // <-- utilise le vrai nom
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                produit.Photo = fileName;
            }


            using var connection = await _connectionProvider.CreateConnection();
            var query = @"INSERT INTO produit (nom_produit, description, stock, prix, id_cat, photo)
                  VALUES (@Nom_produit, @Description, @Stock, @Prix, @Id_cat, @Photo)";
            await connection.ExecuteAsync(query, produit);

            return RedirectToAction("GererArticles");
        }



        [HttpGet]
        public async Task<IActionResult> ModifierArticle(int id)
        {
            using var connection = await _connectionProvider.CreateConnection();
            var produit = await connection.QueryFirstOrDefaultAsync<Produit>("SELECT * FROM produit WHERE id_prod = @id", new { id });

            if (produit == null)
                return NotFound();

            // 🔽 Récupère les noms de fichiers dans wwwroot/images
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            var imageFiles = Directory.GetFiles(imageFolder)
                                      .Select(Path.GetFileName)
                                      .ToList();

            ViewBag.Images = imageFiles;

            return View(produit);
        }


        [HttpPost]
        public async Task<IActionResult> ModifierArticle(Produit produit)
        {
            if (!ModelState.IsValid) return View(produit);

            using var connection = await _connectionProvider.CreateConnection();
            var query = @"UPDATE produit
                          SET nom_produit = @Nom_produit,
                              description = @Description,
                              stock = @Stock,
                              prix = @Prix,
                              id_cat = @Id_cat,
                              photo = @Photo
                          WHERE id_prod = @Id_prod";
            await connection.ExecuteAsync(query, produit);
            return RedirectToAction("GererArticles");
        }

        [HttpPost]
        public async Task<IActionResult> SupprimerArticle(int id)
        {
            using var connection = await _connectionProvider.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM produit WHERE id_prod = @id", new { id });
            return RedirectToAction("GererArticles");
        }
    }
}
