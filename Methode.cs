using System;
using static System.Console;
using static System.Environment;
using System.Threading;

namespace Labyrinth
{
    public abstract class Methode
    {
        
        #region Méthodes utilitaires
        public static void Sortie()
        {
            Clear();
            Chargement("-- Sortie du jeu --");
            Clear();
            Exit(0);
        }
        public static void Pause()
        {
            WriteLine("\nAppuyer sur [ENTRÉE] pour continuer...");
            ReadLine();
        }
        public static void Chargement(string texte)
        {
            for (int i = 1; i < texte.Length-1; i++)
            {
                Clear();
                WriteLine($"{texte}\n");
                int intervalle = 2000/texte.Length;
                ForegroundColor = ConsoleColor.DarkRed;
                char[]chargement = new char[texte.Length];
                for (int k = 0; k <= chargement.Length-1; k++)
                {
                    if (k == 0)chargement[0] = '[';
                    else if(k <=i)chargement[k]='-';
                    else if (k == chargement.Length-1)chargement[k] = ']';
                    else chargement[k]= ' ';
                }
                if (i == chargement.Length-2)
                {
                    ForegroundColor = ConsoleColor.Green;
                    for (int l = 0; l < chargement.Length; l++)Write(chargement[l]);
                    decimal pourcentage = (Convert.ToDecimal(i+2)/Convert.ToDecimal(texte.Length))*100;
                    Write($" {(int)pourcentage} %\n");
                    System.Threading.Thread.Sleep(800);
                }
                else
                {
                    for (int l = 0; l < chargement.Length; l++)Write(chargement[l]);
                    decimal pourcentage = (Convert.ToDecimal(i+2)/Convert.ToDecimal(texte.Length))*100;
                    Write($" {(int)pourcentage} %\n");
                    System.Threading.Thread.Sleep(intervalle);
                }
                ResetColor();
            }
            Clear();
        }
        public static void Titre (string texte,  string texteAdditionnel = "",string texteSpecial = "", int recurrence = 0)
        {
            Clear();
            if (recurrence != 0)
            {
                if (texte != "")WriteLine($"\n{texte}\n");
                if (texteSpecial != "")WriteLine($"\n{texteSpecial}\n");
                if (texteAdditionnel != "")WriteLine($"\n{texteAdditionnel}\n");
            }else
            {
                if (texte != "")WriteLine($"\n{texte}\n");
                if (texteSpecial != "")WriteLine($"\n{texteSpecial}\n");
                if (texteAdditionnel != "") 
                {
                    WriteLine("");
                    for(int i = 0; i < texteAdditionnel.Length; i++)
                    {
                        Write(texteAdditionnel[i]);
                        Thread.Sleep(50);
                        if(KeyAvailable)
                        {
                            ConsoleKeyInfo touche = ReadKey(true);
                            if(touche.Key == ConsoleKey.Escape)Sortie();
                            else if(touche.Key == ConsoleKey.Enter)
                            {
                                Write(texteAdditionnel.Substring(i+1));
                                break;
                            }
                        }
                    }
                    WriteLine("\n");
                }
            }
            
        }

        public static int Selection (string[]entree, string texte, string texteAdditionnel = "", string texteSpecial = "")
        {
            int position = 0;
            bool choixFait = false;
            int recurrence = 0;
            while (!choixFait)
            {
                Clear();
                Methode.Titre(texte,texteAdditionnel,texteSpecial,recurrence);
                string[]selection = TransformationSelection(position, entree);
                for(int i=0;i<selection.Length;i++)
                {
                    if (i == position)
                    { 
                        BackgroundColor = ConsoleColor.Green;
                        WriteLine(selection[i]);
                        ResetColor();
                    }
                    else WriteLine(selection[i]);
                }
                switch(ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:if(position == 0)position = entree.Length-1; else if(position > 0) position--;break;
                    case ConsoleKey.Z:if(position == 0)position = entree.Length-1; else if(position > 0) position--;break;
                    case ConsoleKey.DownArrow: if(position == entree.Length-1)position = 0; else if(position < entree.Length-1)position++;break;
                    case ConsoleKey.S: if(position == entree.Length-1)position = 0; else if(position < entree.Length-1)position++;break;
                    case ConsoleKey.Enter:choixFait = true;break;
                    case ConsoleKey.Escape:Methode.Sortie();break;
                }
                recurrence++;
            }
            return position;
        }
        #endregion
        
        #region Méthodes exercice
        public static string[] TransformationSelection(int position, string[]entree)
        {
            string[]sortie = new string[entree.Length];
            for (int i = 0; i < entree.Length; i++)
            {
                if (i == position)sortie[i] = $" > {entree[i]}";
                else sortie [i]= $"   {entree[i]}";
            }
            return sortie;
        }
        public static void Couleur(int[,]mat, int ancienne, int nouvelle)
        {
            for (int i = 0; i < mat.GetLength(0); i++)for (int j = 0; j < mat.GetLength(1); j++)if (mat[i,j]==ancienne)mat[i,j]=nouvelle;
        }
        public static bool MatriceTerminee(int[,] matrice)
        {
            int valeur = matrice[1,1];
            for (int i = 1; i < matrice.GetLength(0)-1; i++)for (int j = 1; j < matrice.GetLength(1)-1; j++)if (matrice[i, j] != valeur && matrice[i, j] != 1) return false;
            return true;
        }
        public static int[,] RemplirIncrément(int[,]matrice)
        {
            int valeur = 2;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 1; j < matrice.GetLength(0)-1; j++)
                {
                    if (matrice[i,j]==0)
                    {
                        matrice[i,j]=valeur;
                        valeur++;
                    }
                }
            }
            return matrice;
        }
        public static int[,] GenerationMatrice(int n)
        {
            int[,] matrice = new int[n, n];
            for(int i = 0; i < n; i+=2)for(int j = 0; j < n; j++)matrice[i, j] = 1;
            for(int i = 0; i < n; i++)for(int j = 0; j < n; j+=2)matrice[i, j] = 1;
            Methode.RemplirIncrément(matrice);
            Bridge(matrice);
            Methode.Couleur(matrice, matrice[1, 1], 0);
            matrice[1, 0] = 2;
            matrice[n - 2, n - 1]=3;
            return matrice;
        }
        public static void Bridge(int[,]matrice)
        {
            List<Position> positions = new List<Position>();
            Random rnd = new Random();
            bool cond = true;
            int n = matrice.GetLength(0);
            while(!Methode.MatriceTerminee(matrice))
            {
                Position p = new Position(rnd.Next(1, n - 1), rnd.Next(1, n - 1));
                
                if (cond&&!positions.Contains(p))
                {
                    positions.Add(p);
                    if (matrice[p.X,p.Y]==1&&matrice[p.X,p.Y+1]!=1&&matrice[p.X,p.Y-1]!=1)
                    {
                        if (matrice[p.X,p.Y-1]>matrice[p.X,p.Y+1])
                        {
                            Methode.Couleur(matrice,matrice[p.X,p.Y+1],matrice[p.X,p.Y-1]);
                            matrice[p.X,p.Y]=matrice[p.X,p.Y-1];
                    
                        }
                        else if (matrice[p.X,p.Y-1]<matrice[p.X,p.Y+1])
                        {
                            Methode.Couleur(matrice,matrice[p.X,p.Y-1],matrice[p.X,p.Y+1]);
                            matrice[p.X,p.Y]=matrice[p.X,p.Y+1];
                        }
                    }
                    cond = false;
                }
                else if (!positions.Contains(p))
                {
                    positions.Add(p);
                    if (matrice[p.X,p.Y]==1&&matrice[p.X+1,p.Y]!=1&&matrice[p.X-1,p.Y]!=1)
                    {
                        if (matrice[p.X-1,p.Y]>matrice[p.X+1,p.Y])
                        {
                            Methode.Couleur(matrice,matrice[p.X+1,p.Y],matrice[p.X-1,p.Y]);
                            matrice[p.X,p.Y]=matrice[p.X-1,p.Y];
                        }
                        else if (matrice[p.X-1,p.Y]<matrice[p.X+1,p.Y])
                        {
                            Methode.Couleur(matrice,matrice[p.X-1,p.Y],matrice[p.X+1,p.Y]);
                            matrice[p.X,p.Y]=matrice[p.X+1,p.Y];
                        }
                
                    }
                    cond = true;
                }
            }
            Methode.Couleur(matrice, matrice[1, 1], 0);
        }
        public static char SelectionDifficulté()
        {
            switch(Methode.Selection(new string[]{"A - Débutant   ","B - Moyen      ","C - Difficile  ","D - Expert     "}, "-- Lancement de la partie --","Choisir la difficulté du labyrinthe : "))
            {
                case 0:return 'A';
                case 1:return 'B';
                case 2:return 'C';
                case 3:return 'D';
                default : return ' ';
            }
        }
        public static bool DefinirJoueur( Classement classement,Joueur player)
        {
            string[]choixDeJoueur = new string[classement.joueurs.Count+1];
            for(int i = 0; i < choixDeJoueur.Length; i++)
            {
                if(i == 0)choixDeJoueur[0]= "Nouveau joueur...   ";
                else choixDeJoueur[i]= classement.joueurs[i-1].nom;
            }
            int position = Methode.Selection(choixDeJoueur, "-- Selection du joueur --","Veuillez choisir votre pseudonyme, ou le créer s'il n'est pas dans la liste :");
            if (position == 0)
            {
                do
                {
                    Clear();
                    Methode.Titre("-- Selection du joueur --","Veuillez saisir votre pseudo : ");
                    player.nom = ReadLine()+"";
                    if (player.nom.Length<21&&player.nom!="")while(player.nom.Length<20)player.nom+=" ";
                }while (player.nom == "");
            }else
                {
                    player.nom = classement.joueurs[position-1].nom;
                    player.scores = classement.joueurs[position-1].scores;
                    return true;      
                }
            ResetColor();
            Clear();
            return false;
        }
        public static void DebutDeJeu()
        {
            string titre = @"
 _             _           _                _       _   _          
| |           | |         | |              (_)     | | | |         
| |     ___   | |     __ _| |__  _   _ _ __ _ _ __ | |_| |__   ___ 
| |    / _ \  | |    / _` | '_ \| | | | '__| | '_ \| __| '_ \ / _ \
| |___|  __/  | |___| (_| | |_) | |_| | |  | | | | | |_| | | |  __/
\_____/\___|  \_____/\__,_|_.__/ \__, |_|  |_|_| |_|\__|_| |_|\___|
                                  __/ |                            
                                 |___/                             ";
            switch(Methode.Selection(new string[]{"Jouer     ","Options   ","Sortir    "},"", "Bienvenue dans le menu principal ! Utilisez les flèches du clavier pour naviguer et la touche [ENTRÉE] pour valider.",titre))
            {
                case 1 : Personnage.symbole = ChoixSymbole();break;
                case 2 : Methode.Sortie();break;
                default: break;
            }
            Chargement("-- Lancement du jeu --");
        }
        public static char ChoixSymbole ()
        {
            if(Methode.Selection(new string[]{"Oui","Non"},"-- Options --","Le pion par défaut est le π, souhaitez-vous le changer ?")==0)
            {
                Write("\nVeuillez saisir le caractère que vous souhaitez utiliser : ");
                return ReadKey().KeyChar;
            }
            return 'π';
        }

        public static void ReglesDuJeu()
        {
            string regles = @"                                             _______________________
   _______________________-------------------                       `\
 /:--__                                                              |
||< > |                                   ___________________________/
| \__/_________________-------------------                         |
|                                                                  |
 |                       LE JEU DU LABYRINTHE                       |
 |                                                                  |
 |      Le jeu du Labyrinthe est un jeu de réflexion                |
  |        dans lequel le joueur doit trouver le chemin              |
  |      le plus rapide afin de sortir                               |
  |        du labyrinthe. Pour cela, il doit se déplacer             |
  |      dans le labyrinthe en utilisant les touches ZQSD             |
   |       ou les flèches directionnelles.                            |
   |       d : Représente la case de départ.                          |
   |       a : Représente la case d'arrivée.                         |
  |                                              ____________________|_
  |  ___________________-------------------------                      `\
  |/`--_                                                                 |
  ||[ ]||                                            ___________________/
   \===/___________________--------------------------";
            
            Methode.Titre("-- Règles du jeu --", "",regles,1);
            Methode.Pause();

        }     
        #endregion
    
    }
}
