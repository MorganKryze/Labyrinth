using System;
using static System.Console;
using System.Diagnostics;

namespace Labyrinth
{
    public class Program
    {
        static void Main(string[] args)
        {
            Debut :
            #region Démarrage
            Methode.DebutDeJeu();
            bool sessionChoisie = false;
            #endregion
            
            ChangementDeSession :

            #region Création du classement et joueur
            Classement classement = new Classement();
            Joueur player = new Joueur();
            bool playerExiste = Methode.DefinirJoueur(classement, player);
            Joueur.nomActuel = player.nom;
            #endregion
                        
            #region Règles du jeu
            Methode.ReglesDuJeu();
            #endregion
            
            PasDeChangementDeSession :

            #region Génération du plateau 
            if (sessionChoisie)playerExiste = true;
            Plateau plateau = new Plateau();
            Personnage pion = new Personnage(plateau);
            #endregion

            #region Lancement du jeu
            ConsoleKeyInfo touche = new ConsoleKeyInfo();
            while (touche.Key != ConsoleKey.Enter)
            {
                plateau.AffichagePlateau();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nAppuyez sur [ENTRÉE] pour commencer le jeu ! ");
                Console.ResetColor();
                touche = Console.ReadKey();
                if (touche.Key == ConsoleKey.Escape) Methode.Sortie();
            }
            plateau.matrice[pion.positionActuelle.X,pion.positionActuelle.Y] = 5;
            Stopwatch chronometre = new Stopwatch();
            chronometre.Start();
            #endregion

            #region Boucle de jeu
            while (!pion.EstArrive())pion.Deplacement(plateau);
            #endregion

            #region Fin de jeu
            chronometre.Stop();
            TimeSpan temps = chronometre.Elapsed;
            string tempsString = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",temps.Hours, temps.Minutes, temps.Seconds,temps.Milliseconds / 10);
            if(playerExiste)
            {
                int indice =-1;
                for(int i = 0; i < classement.joueurs.Count; i++)if(classement.joueurs[i].nom == player.nom) indice = i;
                switch(Plateau.difficulté)
                    {
                        case 'A' : if (player.scores[0] > temps||player.scores[0]==TimeSpan.Zero) classement.joueurs[indice].scores[0] = temps; break;
                        case 'B' : if (player.scores[1] > temps||player.scores[1]==TimeSpan.Zero) classement.joueurs[indice].scores[1] = temps; break;
                        case 'C' : if (player.scores[2] > temps||player.scores[2]==TimeSpan.Zero) classement.joueurs[indice].scores[2] = temps; break;
                        case 'D' : if (player.scores[3] > temps||player.scores[3]==TimeSpan.Zero) classement.joueurs[indice].scores[3] = temps; break;
                    }
            }
            else
            {
                switch(Plateau.difficulté)
                {
                    case 'A' : player.scores[0] = temps; break;
                    case 'B' : player.scores[1] = temps; break;
                    case 'C' : player.scores[2] = temps; break;
                    case 'D' : player.scores[3] = temps; break;
                }
                classement.joueurs.Add(player);
            }
            StreamWriter sw = new StreamWriter(Classement.path, false);
            foreach(Joueur joueur in classement.joueurs)sw.Write(joueur+"\n");
            sw.Close();
            #endregion

            #region LeaderBoard
            Methode.Titre("-- LeaderBoard --",$"Votre score a été enregistré dans le classement du Labyrinthe {Plateau.difficulté} !");
            for (int i = 0; i < classement.joueurs.Count; i++)
            {
                if(i==0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(classement.TriLead()[i]);
                }else if (i==1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(classement.TriLead()[i]);
                }else if (i==2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(classement.TriLead()[i]);
                    Console.ResetColor();
                }else Console.WriteLine(classement.TriLead()[i]);
                
            }
            Console.Write($"Votre score est de {tempsString}!\n");
            
            Methode.Pause();
            #endregion
            
            #region Nouvelle partie
            if (Methode.Selection(new string[]{"oui","non"}, "-- Fin de partie --","Souhaitez-vous jouer à nouveau ?")==0)
            {
                if (Methode.Selection(new string[]{"oui","non"},"-- Selection de la session --",$"Souhaitez-vous continuer avec la session de jeu {Joueur.nomActuel}")==0)
                {
                    sessionChoisie=true;
                    goto PasDeChangementDeSession;
                }
                else 
                {
                    sessionChoisie = false;
                    goto ChangementDeSession;
                }
            }else goto Debut;
            #endregion
        }
    }
}
