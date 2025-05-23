using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "L'email est requis.")]
        [Display(Name = "email")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [Display(Name = "mot de passe")]
        public string Mdp { get; set; }
    }

}
