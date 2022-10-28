using System;
using System.IO;
using static System.Environment;

namespace Labyrinth
{
    public class Classement
    {
        
        #region Attributs
        public List<Joueur> joueurs {get; set;}
        public static string path= GetEnvironmentVariable("ROOT_LEADERBOARD")+"LeaderBoard.txt";
        #endregion

        #region Constructeur
        public Classement()
        {
            joueurs = new List<Joueur>();
            string[] lignesDocument = File.ReadAllLines(path);
            for (int i = 0; i < lignesDocument.Length; i++)
            {
                joueurs.Add(new Joueur());
                string[] infos = lignesDocument[i].Split(';');
                joueurs[i].nom = infos[0];
                for (int j = 1; j < infos.Length; j++)joueurs[i].scores[j-1] = TimeSpan.Parse(infos[j]);
            }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            string retour = "";
            for (int i = 0; i < joueurs.Count; i++)
            {
                retour += joueurs[i].nom + " : ";
                for (int j = 0; j < joueurs[i].scores.Length; j++)
                {
                    retour += joueurs[i].scores[j] + " ; ";
                }
                retour += "\n";
            }
            return retour;
        }
        public string[] TriLead()
        {
            string listeDifficultés = "ABCD";
            int index = listeDifficultés.IndexOf(Plateau.difficulté);
            List<Joueur> joueursTriés = new List<Joueur>();
            for (int i = 0; i < joueurs.Count; i++)if (joueurs[i].scores[index] != TimeSpan.Zero)joueursTriés.Add(joueurs[i]);
            string[] retour = new string[joueurs.Count];
            joueursTriés.Sort((x, y) => x.scores[index].CompareTo(y.scores[index]));
            for (int i = 0; i < joueursTriés.Count; i++)
            { 
                if(i<9)retour[i] = $"0{i+1} - {joueursTriés[i].nom} : {joueursTriés[i].scores[index]}";
                else retour[i] = $"{i+1} - {joueursTriés[i].nom} : {joueursTriés[i].scores[index]}";
            }
            return retour;
        }
        #endregion
    
    }
}
