using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projet_salle_de_gym.Models.Auth;
using Projet_salle_de_gym.Models;
using Projet_salle_de_gym.Core.Infrastructure;
using Dapper;
using Projet_salle_de_gym.Models.Utilisateurs;

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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            using var connection = await _connectionProvider.CreateConnection();
            var utilisateur = await connection.QueryFirstOrDefaultAsync<Utilisateur>(
                "SELECT * FROM utilisateur WHERE mail = @mail",
                new { mail = model.Mail });

            if (utilisateur == null || utilisateur.Mdp != model.Mdp)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect.");
                return View();
            }

            // Stocker prénom et rôle dans la session
            HttpContext.Session.SetString("Prenom", utilisateur.Prenom_util);
            HttpContext.Session.SetString("Nom", utilisateur.Nom_util);
            HttpContext.Session.SetString("Admin", utilisateur.Admin.ToString());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UtilisateurRegisterModel model)
        {
            if (!ModelState.IsValid)
                return View();

            using var connection = await _connectionProvider.CreateConnection();

            var query = @"INSERT INTO utilisateur (nom_util, prenom_util, mail, mdp, admin)
                          VALUES (@Nom_util, @Prenom_util, @Mail, @Mdp, false)";

            await connection.ExecuteAsync(query, new
            {
                model.Nom_util,
                model.Prenom_util,
                model.Mail,
                model.Mdp
            });

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Vide toute la session
            return RedirectToAction("Index", "Home");
        }
    }
}