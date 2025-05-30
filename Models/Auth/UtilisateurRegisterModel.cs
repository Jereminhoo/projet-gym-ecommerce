﻿using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models.Auth
{
    public class UtilisateurRegisterModel
    {
        [Required(ErrorMessage = "Le champ ne peut pas être vide"), StringLength(50, ErrorMessage = "Caractères max dépassé")]
        [Display(Name = "Nom")]
        public string Nom_util { get; set; }

        [Required(ErrorMessage = "Le champ ne peut pas être vide"), StringLength(50, ErrorMessage = "Caractères max dépassé")]
        [Display(Name = "Prénom")]
        public string Prenom_util { get; set; }

        [Required (ErrorMessage = "Le champ ne peut pas être vide")]
        [Display(Name = "Email")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Le champ ne peut pas être vide"), StringLength(100, MinimumLength = 4,ErrorMessage="Le mot de passe doit être au minimum de 4 caractères")]
        [Display(Name = "Mot de passe")]
        public string Mdp { get; set; }

        [Compare("Mdp", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        [Required(ErrorMessage = "Le champ ne peut pas être vide")]
        [Display(Name = "Confirmer le mot de passe")]
        public string ConfirmMdp { get; set; }
    }
}
