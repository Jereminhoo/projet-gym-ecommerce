using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models
{
    public class Inscription : IValidatableObject
    {
        [Required, StringLength(50)]
        [Display(Name = "Nom d'utilisateur")]
        public string Username { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string mail { get; set; }

        [Required, StringLength(100, MinimumLength = 3)]
        [Display(Name = "Mot de passe")]
        public string mdp { get; set; }

        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        [Display(Name = "Confirmer le mot de passe")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Nom")]
        public string Nom { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
