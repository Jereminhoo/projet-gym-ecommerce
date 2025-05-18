namespace Projet_salle_de_gym.Models.Products
{
    public class Produit
    {
        public int Id_prod { get; set; }
        public string Nom_produit { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Prix { get; set; }
        public int Id_cat { get; set; }
        public string Photo { get; set; }
    }
}
