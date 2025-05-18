using Dapper;
using Microsoft.AspNetCore.Mvc;
using Projet_salle_de_gym.Core.Infrastructure;
using Projet_salle_de_gym.Models.Auth;

namespace Projet_salle_de_gym.Controllers
{
    public class AuthController : Controller
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public AuthController(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UtilisateurRegisterModel model)
        {
            using var connection = await _connectionProvider.CreateConnection();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Mdp);

            var query = @"INSERT INTO utilisateur (nom_util, prenom_util, mail, mdp, admin)
                          VALUES (@Nom_util, @Prenom_util, @Mail, @Mdp, false)";

            await connection.ExecuteAsync(query, new
            {
                model.Nom_util,
                model.Prenom_util,
                model.Mail,
                Mdp = hashedPassword
            });

            return RedirectToAction("Index", "Home");
        }
    }
}