using System;

namespace Labyrinth
{
    public class Joueur
    {
        #region Attributs
        public string nom {get; set;}
        public static string? nomActuel {get; set;}
        public TimeSpan[]scores {get; set;}
        #endregion
        
        #region Constructeur
        public Joueur()
        {
            nom = "";
            nomActuel = "";
            scores = new TimeSpan[4];
        }
        #endregion

        #region Méthode utilitaire
        public override string ToString()
        {
            string retour = nom;
            for (int i = 0; i < scores.Length; i++)
            {
                retour += ";" + scores[i] ;
            }
            return retour;
        }
        #endregion
    
    }
}
