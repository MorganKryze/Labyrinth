using System;
using static System.Console;

namespace Labyrinth
{
    public class Plateau
    {
        
        #region Attributs
        public int[,] matrice {get; set; }
        public static char difficulté {get; set; }
        public int taille {get; set; }
        public Position depart {get; private set;}
        public  Position arrivee {get; private set;}

        #endregion

        #region Constructeur
        public Plateau()
        {
            depart = new Position(0,0);
            arrivee = new Position(0,0);
            difficulté = Methode.SelectionDifficulté();
            switch (difficulté)
            {
                    case 'A' : taille = 5; break;
                    case 'B' : taille = 15; break;
                    case 'C' : taille = 35; break;
                    case 'D' : taille = 55; break;
            }
            matrice = Methode.GenerationMatrice(taille);
            for(int i = 0; i < taille ; i++)
            {
                for(int j = 0; j < taille; j++)
                {
                    if (matrice[i,j]==2)depart = new Position(i,j);
                    else if (matrice[i,j]==3)arrivee = new Position(i,j);
                }
            }
        }
        #endregion

        #region Méthodes utilitaires
        public override string ToString()
        {
            string retour = "";
            for(int i = 0; i < matrice.GetLength(0) ; i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    switch(matrice[i,j])
                    {
                        case 0 : retour += ' ';break;
                        case 1 : retour += '#';break;
                        case 2 : retour += 'd';break;
                        case 3 : retour += 'a';break;
                        case 4 : retour += '.';break;
                        case 5 : retour += Personnage.symbole;break; 
                        default : break;
                    }
                    retour+= " ";
                }
                retour +="\n";
            }
            return retour;
        }
        public void AffichagePlateau()
        {
            Clear();
            for(int i = 0; i < taille ; i++)
            {
                BackgroundColor = ConsoleColor.Green;
                if (i == 1||i == matrice.GetLength(0)-2)
                {
                    for (int j = 0; j < taille; j++)
                    {
                    switch(matrice[i,j])
                    {
                        case 0 : Write(" ");break;
                        case 1 : Write("#");break;
                        case 2 : BackgroundColor = ConsoleColor.DarkRed;ForegroundColor = ConsoleColor.White;Write("d");break;
                        case 3 : BackgroundColor = ConsoleColor.DarkBlue;ForegroundColor = ConsoleColor.White;Write("a");break;
                        case 4 : Write(".");break;
                        case 5 : Write(Personnage.symbole);break; 
                        default : break;
                    }
                    BackgroundColor = ConsoleColor.Green;
                    ForegroundColor = ConsoleColor.Black;
                    Write(" ");
                    }
                    ResetColor();
                    WriteLine();
                }
                else 
                {
                    string retour = "";
                    for (int j = 0; j < taille; j++)
                    {
                        switch(matrice[i,j])
                        {
                            case 0 : retour += ' ';break;
                            case 1 : retour += '#';break;
                            case 2 : retour += 'd';break;
                            case 3 : retour += 'a';break;
                            case 4 : retour += '.';break;
                            case 5 : retour += Personnage.symbole;break; 
                            default : break;
                        }
                        retour+= " ";
                    }
                    Console.WriteLine(retour);
                }
            }
            Console.ResetColor();
        }
        #endregion
        
        #region Méthodes
        public bool MarquerPassage(Position position)
        {
            switch(matrice[position.X,position.Y])
            {
                case 1 : return false;
                case 5 : return false;
                default : return true;
            }
        }
        #endregion
        
    
    }
}
