using System;

namespace Labyrinth
{
    /// <summary>The position class</summary>
    class Position
    {
        #region Attributes
        /// <summary>The x coordinate of the position.</summary>
        public int X {get; set;}
        /// <summary>The y coordinate of the position.</summary>
        public int Y {get; set;}
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="T:Labyrinth.Position"/> class.</summary>
        public Position (int a, int b)
        {
            X = a;
            Y = b;
        }
        #endregion

        #region Methods
        /// <summary>Converts the position to a string.</summary>
        public override string ToString()
        {
            return $"Line : {X} ; Column {Y}";
        }
        /// <summary>Checks if the position is equal to another position.</summary>
        public bool Equals(Position b)
        {
            return (X == b.X && Y == b.Y);
        }
        #endregion
    }
}
