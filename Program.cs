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
            while (!pawn.CurrentPosition.Equals(pawn.ArrivalPosition))if (pawn.Displacement(board)) goto Board_Creation;
            #endregion

            #region End of the game
            timer.Stop();
            TimeSpan time = timer.Elapsed;
            string timeToString = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",time.Hours, time.Minutes, time.Seconds,time.Milliseconds / 10);
            if(!player.IsNewPlayer())
            {
                int index =player.IndexOfPlayer();
                if (ranking.PlayersList[index].Scores[Board.s_Difficulty] == TimeSpan.Zero || ranking.PlayersList[index].Scores[Board.s_Difficulty] > time)ranking.PlayersList[index].Scores[Board.s_Difficulty] = time;
                    
            }else
            {
                player.Scores[Board.s_Difficulty] = time;
                ranking.PlayersList.Add(player);
            }
            string[]playersListToString = new string [ranking.PlayersList.Count];
            foreach (Player p in ranking.PlayersList)playersListToString[ranking.PlayersList.IndexOf(p)] = p.ToString();
            WriteAllLines(Ranking.s_StoredPath,playersListToString);
            #endregion

            #region LeaderBoard
            Methods.Title("-- LeaderBoard --",$"Your score has been recorded in the Labyrinth leaderboard number {Board.s_Difficulty+1}!");
            string[]rankingToString = ranking.LeaderBoardSorting();
            BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < rankingToString.Length; i++)
            {
                ForegroundColor = ConsoleColor.White;
                if(i==0)
                {
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine(rankingToString[i]);
                }else if (i==1)
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine(rankingToString[i]);
                }else if (i==2)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine(rankingToString[i]);
                    
                }else Console.WriteLine(rankingToString[i]);
                
            }
            Write($"\nYour current score is {timeToString}!\n");
            Methods.Pause();
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
