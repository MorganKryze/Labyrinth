using System;

namespace Labyrinth
{
    public class Position
    {
        
        #region Attributs
        public int X {get; set;}
        public int Y {get; set;}
        #endregion

        #region Constructeur
        public Position (int a, int b)
        {
            X = a;
            Y = b;
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return $"Ligne : {X} ; Colonne {Y}";
        }
        public bool Equals(Position b)
        {
            return (X == b.X && Y == b.Y);
        }
        #endregion
    
    }
}
