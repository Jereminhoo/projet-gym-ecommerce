using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models.Auth
{
    public class UtilisateurRegisterModel
    {
        [Required, StringLength(50)]
        [Display(Name = "Nom")]
        public string Nom_util { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Prénom")]
        public string Prenom_util { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Mail { get; set; }

        [Required, StringLength(100, MinimumLength = 4)]
        [Display(Name = "Mot de passe")]
        public string Mdp { get; set; }

        [Compare("Mdp", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        [Display(Name = "Confirmer le mot de passe")]
        public string ConfirmMdp { get; set; }
    }
}
