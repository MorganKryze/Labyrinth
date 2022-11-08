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
        /// <summary>The positon of the board bonus.</summary>
        public Position BonusPosition {get; set;}
        /// <summary>Whether the pawn has the bonus or not.</summary>
        public static bool s_BonusTaken {get; set;}
        /// <summary>The symbol of the bonus.</summary>
        public static char s_BonusSymbol = '';
        /// <summary>The symbol of the pawn.</summary>
        public static char s_PawnSymbol = '▶';

        #endregion

        #region Constructor
        /// <summary>This method is used to initialize a new instance of the <see cref="T:Labyrinth.GamePawn"/> class.</summary>
        /// <param name="labyrinth">The generated board for the game.</param>
        public GamePawn(Board labyrinth)
        {
            CurrentPosition = labyrinth.Start;
            ArrivalPosition = labyrinth.End;
            BonusPosition = labyrinth.Bonus;
            s_BonusTaken = false;
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
        public int Displacement(Board labyrinth)
        {
            labyrinth.PrintBoard();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Z :case ConsoleKey.UpArrow : Movement(labyrinth,1); break;
                case ConsoleKey.Q : case ConsoleKey.LeftArrow : Movement(labyrinth,2); break;
                case ConsoleKey.S : case ConsoleKey.DownArrow : Movement(labyrinth,3); break;
                case ConsoleKey.D : case ConsoleKey.RightArrow : Movement(labyrinth,4); break;
                case ConsoleKey.Escape : return -1;
            }
            return 1;
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
                    s_PawnSymbol = '▲';
                    if (labyrinth.IsAvailable(TemporaryPosition1))
                    {
                        labyrinth.Matrix[TemporaryPosition1.X,TemporaryPosition1.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition1;
                    }else TemporaryPosition1.X+=1;
                break;
                case 2 : 
                    s_PawnSymbol = '◀';
                    if(CurrentPosition.Equals(labyrinth.Start))break;
                    else 
                    {
                        Position TemporaryPosition2 = new Position(CurrentPosition.X,CurrentPosition.Y-1);
                        if (labyrinth.IsAvailable(TemporaryPosition2))
                        {
                            labyrinth.Matrix[TemporaryPosition2.X,TemporaryPosition2.Y] = 5;
                            labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                            CurrentPosition = TemporaryPosition2;
                            
                        }else TemporaryPosition2.Y+=1;
                    }
                break;
                case 3 : 
                    Position TemporaryPosition3 = new Position(CurrentPosition.X+1,CurrentPosition.Y);
                    s_PawnSymbol = '▼';
                    if (labyrinth.IsAvailable(TemporaryPosition3))
                    {
                        labyrinth.Matrix[TemporaryPosition3.X,TemporaryPosition3.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition3;
                    }else TemporaryPosition3.X-=1;
                break;
                case 4 :
                    Position TemporaryPosition4 = new Position(CurrentPosition.X,CurrentPosition.Y+1);
                    s_PawnSymbol = '▶';
                    if (labyrinth.IsAvailable(TemporaryPosition4))
                    {
                        labyrinth.Matrix[TemporaryPosition4.X,TemporaryPosition4.Y] = 5;
                        labyrinth.Matrix[CurrentPosition.X,CurrentPosition.Y] = 4;
                        CurrentPosition = TemporaryPosition4;
                    }else TemporaryPosition4.Y-=1;
                break;
            }
        }
        /// <summary>This method is used to change the pawn symbol.</summary>
        /// <returns>The symbol of the pawn as a char.</returns>
        public static char ChangeSymbol ()
        {
            if(Methods.ScrollingMenu(new string[]{"Yes ","No "},"-- Options --",$"The default symbol for the bonus is {GamePawn.s_BonusSymbol}, would you like to change it?")==0)
            {
                Write("\nPlease type your replacement character: ");
                return ReadKey().KeyChar;
            }
            return '';
        }
        #endregion


    }
}
