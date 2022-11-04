using System;
using static System.Console;
using static System.Environment;

namespace Labyrinth
{
    /// <summary>The game pawn class</summary>
    class GamePawn
    {
        
        #region Attributes
        /// <summary>The current position of the pawn.</summary>
        public Position CurrentPosition {get; set;}
        /// <summary>The final position of the pawn.</summary>
        public Position ArrivalPosition {get; set;}
        /// <summary>The symbol of the pawn.</summary>
        public static char symbol = 'π';

        #endregion

        #region Constructor
        /// <summary>This method is used to initialize a new instance of the <see cref="T:Labyrinth.GamePawn"/> class.</summary>
        /// <param name="labyrinth">The generated board for the game.</param>
        public GamePawn(Board labyrinth)
        {
            CurrentPosition = labyrinth.Start;
            ArrivalPosition = labyrinth.End;
        }
        #endregion

        #region Utility Methods
        /// <summary>This method is used to give a string of the gamepawn.</summary>
        /// <returns>A string of the current position.</returns>
        public override string ToString() => CurrentPosition.ToString();
        #endregion

        #region Methods
        /// <summary>This method is used to recieve the keypressed, and make the pawn move in consequence.</summary>
        /// <param name="labyrinth">The generated board for the game.</param>
        /// <returns>Wether the player has pressed [ESCAPE] and wants to go to the previous page or not.</returns>
        public bool Displacement(Board labyrinth)
        {
            labyrinth.PrintBoard();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Z :case ConsoleKey.UpArrow : Movement(labyrinth,1); break;
                case ConsoleKey.Q : case ConsoleKey.LeftArrow : Movement(labyrinth,2); break;
                case ConsoleKey.S : case ConsoleKey.DownArrow : Movement(labyrinth,3); break;
                case ConsoleKey.D : case ConsoleKey.RightArrow : Movement(labyrinth,4); break;
                case ConsoleKey.Escape : return true;
            }
            return false;
        }
        /// <summary>This method is used to move the pawn if the next position is available.</summary>
        /// <param name="labyrinth">The generated board for the game.</param>
        /// <param name="direction">The direction in which the pawn is moving.</param>
        public void Movement (Board labyrinth, int direction)
        {
            switch(direction)
            {
                case 1 : 
                    Position TemporaryPosition1 = new Position(CurrentPosition.X-1,CurrentPosition.Y);
                    if (labyrinth.IsAvailable(TemporaryPosition1))
                    {
                        labyrinth.Matrix[TemporaryPosition1.X,TemporaryPosition1.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition1;
                    }else TemporaryPosition1.X+=1;
                break;
                case 2 : 
                    Position TemporaryPosition2 = new Position(CurrentPosition.X,CurrentPosition.Y-1);
                    if (TemporaryPosition2 != new Position(0,0)&&labyrinth.IsAvailable(TemporaryPosition2))
                    {
                        labyrinth.Matrix[TemporaryPosition2.X,TemporaryPosition2.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition2;
                    }else TemporaryPosition2.Y+=1;
                break;
                case 3 : 
                    Position TemporaryPosition3 = new Position(CurrentPosition.X+1,CurrentPosition.Y);
                    if (labyrinth.IsAvailable(TemporaryPosition3))
                    {
                        labyrinth.Matrix[TemporaryPosition3.X,TemporaryPosition3.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition3;
                    }else TemporaryPosition3.X-=1;
                break;
                case 4 :
                    Position TemporaryPosition4 = new Position(CurrentPosition.X,CurrentPosition.Y+1);
                    if (labyrinth.IsAvailable(TemporaryPosition4))
                    {
                        labyrinth.Matrix[TemporaryPosition4.X,TemporaryPosition4.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition4;
                    }else TemporaryPosition4.Y-=1;
                break;
            }
        }
        #endregion


    }
}
