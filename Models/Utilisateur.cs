namespace Projet_salle_de_gym.Models
{
    public class Utilisateur
    {
        public int Id_utilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string mdp { get; set; }
        public string mail { get; set; }
        public bool admin { get; set; }
    }
}
