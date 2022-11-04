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
            Board.s_Difficulty = Methods.ScrollingMenu(new string[]{"A - Beginner   ","B - Medium     ","C - Tricky     ","D - Expert     "}, "-- Setting the difficulty --","PLease choose wisely the difficulty of the labyrinth: ");
            if(Board.s_Difficulty == -1) goto Player_Creation;
            Board plateau = new Board();
            GamePawn pawn = new GamePawn(plateau);
            #endregion

            #region Start of the game
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Enter)
            {
                plateau.PrintBoard();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nPress [ENTER] to start the game! ");
                Console.ResetColor();
                keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Escape) goto Board_Creation;
            }
            plateau.Matrix[pawn.CurrentPosition.X,pawn.CurrentPosition.Y] = 5;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            #endregion

            #region Game loop
            while (!pawn.CurrentPosition.Equals(pawn.ArrivalPosition))if (pawn.Displacement(plateau)) goto Board_Creation;
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
            WriteAllLines(Ranking.s_StoragePath,playersListToString);
            #endregion

            #region LeaderBoard
            Methods.Title("-- LeaderBoard --",$"Your score has been recorded in the Labyrinth leaderboard number {Board.s_Difficulty+1}!");
            string[]rankingToString = ranking.LeaderBoardSorting();
            for (int i = 0; i < rankingToString.Length; i++)
            {
                if(i==0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(rankingToString[i]);
                }else if (i==1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(rankingToString[i]);
                }else if (i==2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(rankingToString[i]);
                    Console.ResetColor();
                }else Console.WriteLine(rankingToString[i]);
                
            }
            Console.Write($"\nYour current score is {timeToString}!\n");
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
