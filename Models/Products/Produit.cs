using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models.Products
{
    public class Produit
    {
        public int Id_prod { get; set; }

        [Required(ErrorMessage = "Le nom du produit est requis.")]
        [Display(Name = "Nom du produit")]
        public string Nom_produit { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Le stock est requis.")]
        [Range(1, int.MaxValue, ErrorMessage = "Le stock doit être supérieur à 0.")]
        [Display(Name = "Stock")]
        public int? Stock { get; set; }  

        [Required(ErrorMessage = "Le prix est requis.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        [Display(Name = "Prix")]
        public decimal? Prix { get; set; } 

        [Required(ErrorMessage = "La catégorie est requise.")]
        [Display(Name = "Id catégorie")]
        public int? Id_cat { get; set; }

        [Display(Name = "Photo")]
        public string? Photo { get; set; }
    }
}
