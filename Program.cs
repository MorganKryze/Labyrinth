using System;
using System.Diagnostics;
using static System.Console;
using static System.IO.File;

namespace Labyrinth
{
    /// <summary>The main class where the core of the program is.</summary>
    class Program
    {
        /// <summary>The main method.</summary>
        public static void Main()
        {
            #region Config
            Methods.ConsoleConfig();
            #endregion

            Main_Menu :

            #region Lobby
            Methods.MainMenu();
            #endregion
            
            Player_Creation :

            #region Setting up the player
            Ranking ranking = new Ranking();
            Player player = new Player();
            if (Methods.DefineSession(ranking, player)) goto Main_Menu;
            Methods.Rules();
            #endregion
            
            Board_Creation :

            #region Setting up the board
            Board.s_Difficulty = Methods.ScrollingMenu(new string[]{"A - Beginner   ","B - Medium     ","C - Tricky     ","D - Expert     "}, "-- Setting the difficulty --","Please choose wisely the difficulty of the labyrinth: ");
            if(Board.s_Difficulty == -1) goto Player_Creation;
            Board board = new Board();
            GamePawn pawn = new GamePawn(board);
            #endregion

            #region Start of the game
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Enter)
            {
                board.PrintBoard();
                BackgroundColor = ConsoleColor.DarkRed;
                ForegroundColor = ConsoleColor.White;
                WriteLine("\nPress [ENTER] to start the game! ");
                BackgroundColor = ConsoleColor.Black;
                keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Escape) goto Board_Creation;
            }
            board.Matrix[pawn.CurrentPosition.X,pawn.CurrentPosition.Y] = 5;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            #endregion

            #region Game loop
            while (!pawn.CurrentPosition.Equals(pawn.ArrivalPosition))
            {
                if (pawn.Displacement(board)==-1) goto Board_Creation;
                else if (pawn.CurrentPosition.Equals(pawn.BonusPosition))
                {
                    GamePawn.s_BonusTaken = true;
                    pawn.BonusPosition = new Position(-1,-1);
                }
            }
            timer.Stop();
            #endregion

            #region End of the game
            Methods.LeaderBoardCreation( player, timer,ranking);
            #endregion

            #region New game ?
            if (Methods.ScrollingMenu(new string[]{"Yes ","No  "}, "-- End of the game --","Do you want to play agin?")==0)
            {
                if (Methods.ScrollingMenu(new string[]{"Yes ","No  "},"-- Session selection --",$"Do you want to stay connected to the session {Player.s_SessionName}?")==0)goto Board_Creation;
                else goto Player_Creation;
            }else goto Main_Menu;
            #endregion
        }
    }
}
