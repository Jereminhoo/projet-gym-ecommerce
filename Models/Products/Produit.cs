using System.ComponentModel.DataAnnotations;

namespace Projet_salle_de_gym.Models.Products
{
    public class Produit
    {
        public int Id_prod { get; set; }

        [Required]
        public string Nom_produit { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Prix { get; set; }

        [Required]
        public int Id_cat { get; set; }

        public string Photo { get; set; }
    }
}
