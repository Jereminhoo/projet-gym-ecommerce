using Dapper;
using Microsoft.AspNetCore.Mvc;
using Projet_salle_de_gym.Core.Infrastructure;
using Projet_salle_de_gym.Models.Utilisateurs;

namespace Projet_salle_de_gym.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public AdminController(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using var connection = await _connectionProvider.CreateConnection();


            var utilisateurs = (await connection.QueryAsync<Utilisateur>(
            "SELECT * FROM utilisateur WHERE admin = false"));


            return View(utilisateurs.ToList()); 
        }
        
    }
}