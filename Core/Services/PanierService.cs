using Projet_salle_de_gym.Models.Panier;
using System.Text.Json;


namespace Projet_salle_de_gym.Core.Services
{
    public static class PanierService
    {
        private const string PanierKey = "Panier";

        public static List<PanierItem> GetPanier(ISession session)
        {
            var json = session.GetString(PanierKey);
            return json != null
                ? JsonSerializer.Deserialize<List<PanierItem>>(json)
                : new List<PanierItem>();
        }

        public static void SavePanier(ISession session, List<PanierItem> panier)
        {
            var json = JsonSerializer.Serialize(panier);
            session.SetString(PanierKey, json);
        }

        public static void AjouterProduit(ISession session, PanierItem item)
        {
            var panier = GetPanier(session);
            var existant = panier.FirstOrDefault(p => p.IdProduit == item.IdProduit);

            if (existant != null)
                existant.Quantite += item.Quantite;
            else
                panier.Add(item);

            SavePanier(session, panier);
        }

        public static void SupprimerProduit(ISession session, int idProduit)
        {
            var panier = GetPanier(session);
            var item = panier.FirstOrDefault(p => p.IdProduit == idProduit);
            if (item != null)
            {
                panier.Remove(item);
                SavePanier(session, panier);
            }
        }

    }
}