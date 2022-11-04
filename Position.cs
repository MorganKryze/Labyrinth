using System;

namespace Labyrinth
{
    public class Position
    {
        #region Attributes
        public int X {get; set;}
        public int Y {get; set;}
        #endregion

        #region Constructor
        public Position (int a, int b)
        {
            X = a;
            Y = b;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Line : {X} ; Column {Y}";
        }

        public bool Equals(Position b)
        {
            return (X == b.X && Y == b.Y);
        }
        #endregion
    }
}
