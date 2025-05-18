namespace Projet_salle_de_gym.Models.Utilisateurs
{
    public class Utilisateur
    {
        public int Id_util { get; set; }
        public string Nom_util { get; set; }
        public string Prenom_util { get; set; }
        public string Mdp { get; set; }
        public string Mail { get; set; }
        public bool Admin { get; set; }
    }
}
