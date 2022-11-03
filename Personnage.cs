using System;
using static System.Console;
using static System.Environment;

namespace Labyrinth
{
    public class Personnage
    {
        
        #region Attributs
        public Position positionActuelle {get; set;}
        public Position positionFinale {get; set;}
        public static char symbole = 'π';

        #endregion

        #region Constructeur
        public Personnage(Plateau labyrinthe)
        {
            positionActuelle = labyrinthe.depart;
            positionFinale = labyrinthe.arrivee;
        }

        #endregion

        #region Méthodes utilitaires
        public override string ToString() => $"({positionActuelle.X},{positionActuelle.Y})";
        public bool EstArrive() => positionActuelle.Equals(positionFinale);
        #endregion

        #region Méthodes
        /* archive méthode
        
        public void Deplacement(Plateau labyrinthe)
        {
            labyrinthe.AffichagePlateau();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Z :case ConsoleKey.UpArrow : Mouvement(labyrinthe,1); break;
                case ConsoleKey.Q : case ConsoleKey.LeftArrow : Mouvement(labyrinthe,2); break;
                case ConsoleKey.S : case ConsoleKey.DownArrow : Mouvement(labyrinthe,3); break;
                case ConsoleKey.D : case ConsoleKey.RightArrow : Mouvement(labyrinthe,4); break;
                case ConsoleKey.Escape : Methode.Sortie(); break;
            }
        }*/
        public bool Deplacement(Plateau labyrinthe)
        {
            labyrinthe.AffichagePlateau();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Z :case ConsoleKey.UpArrow : Mouvement(labyrinthe,1); break;
                case ConsoleKey.Q : case ConsoleKey.LeftArrow : Mouvement(labyrinthe,2); break;
                case ConsoleKey.S : case ConsoleKey.DownArrow : Mouvement(labyrinthe,3); break;
                case ConsoleKey.D : case ConsoleKey.RightArrow : Mouvement(labyrinthe,4); break;
                case ConsoleKey.Escape : return true;
            }
            return false;
        }
        public void Mouvement (Plateau labyrinthe, int cas)
        {
            switch(cas)
            {
                case 1 : 
                    Position testPosition1 = new Position(positionActuelle.X-1,positionActuelle.Y);
                    if (labyrinthe.MarquerPassage(testPosition1))
                    {
                        labyrinthe.matrice[testPosition1.X,testPosition1.Y] = 5;
                        labyrinthe.matrice[positionActuelle.X,positionActuelle.Y] = 4;
                        positionActuelle = testPosition1;
                    }else testPosition1.X+=1;
                break;
                case 2 : 
                    Position testPosition2 = new Position(positionActuelle.X,positionActuelle.Y-1);
                    if (testPosition2 != new Position(0,0)&&labyrinthe.MarquerPassage(testPosition2))
                    {
                        labyrinthe.matrice[testPosition2.X,testPosition2.Y] = 5;
                        labyrinthe.matrice[positionActuelle.X,positionActuelle.Y] = 4;
                        positionActuelle = testPosition2;
                    }else testPosition2.Y+=1;
                break;
                case 3 : 
                    Position testPosition3 = new Position(positionActuelle.X+1,positionActuelle.Y);
                    if (labyrinthe.MarquerPassage(testPosition3))
                    {
                        labyrinthe.matrice[testPosition3.X,testPosition3.Y] = 5;
                        labyrinthe.matrice[positionActuelle.X,positionActuelle.Y] = 4;
                        positionActuelle = testPosition3;
                    }else testPosition3.X-=1;
                break;
                case 4 :
                    Position testPosition4 = new Position(positionActuelle.X,positionActuelle.Y+1);
                    if (labyrinthe.MarquerPassage(testPosition4))
                    {
                        labyrinthe.matrice[testPosition4.X,testPosition4.Y] = 5;
                        labyrinthe.matrice[positionActuelle.X,positionActuelle.Y] = 4;
                        positionActuelle = testPosition4;
                    }else testPosition4.Y-=1;
                break;
            }
        }
        #endregion


    }
}
