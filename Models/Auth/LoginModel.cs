using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string Mail { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        public string Mdp { get; set; }
    }

}
