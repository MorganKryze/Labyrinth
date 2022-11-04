using System;
using static System.Console;
using static System.Environment;

namespace Labyrinth
{
    public class GamePawn
    {
        
        #region Attributes
        public Position CurrentPosition {get; set;}
        public Position ArrivalPosition {get; set;}
        public static char symbol = 'π';

        #endregion

        #region Constructor
        public GamePawn(Board labyrinthe)
        {
            CurrentPosition = labyrinthe.Start;
            ArrivalPosition = labyrinthe.End;
        }

        #endregion

        #region Utility Methods
        public override string ToString() => CurrentPosition.ToString();
        #endregion

        #region Methods
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
