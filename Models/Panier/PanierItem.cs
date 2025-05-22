namespace Projet_salle_de_gym.Models.Panier
{
    public class PanierItem
    {
        public int IdProduit { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public DateTime DateCommande { get; set; }

    }
}
