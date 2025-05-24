using Microsoft.AspNetCore.Mvc;
using Projet_salle_de_gym.Core.Infrastructure;
using Projet_salle_de_gym.Models.Products;
using Dapper;

namespace Projet_salle_de_gym.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public HomeController(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IActionResult> Index(int? id_cat)
        {
            using var connection = await _connectionProvider.CreateConnection();

            var query = id_cat.HasValue
                ? "SELECT * FROM produit WHERE id_cat = @id_cat AND stock > 0"
                : "SELECT * FROM produit WHERE stock > 0";

            var produits = await connection.QueryAsync<Produit>(query, new { id_cat });

            return View(produits.ToList());
        }

    }
}
