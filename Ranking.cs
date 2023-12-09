using System;
using System.IO;
using static System.Environment;
using static System.Console;

namespace Labyrinth
{
    /// <summary>The ranking class</summary>
    class Ranking
    {
        
        #region Attributes
        /// <summary>The list in which the players are stored.</summary>
        public List<Player> PlayersList {get; set;}
        /// <summary>The path of the file where the ranking is stored.</summary>
        public static string s_StoredPath = "LeaderBoard.txt";
        #endregion

        #region Constructor
        /// <summary>This method initializes a new instance of the <see cref="T:Labyrinth.Ranking"/> class.</summary>
        public Ranking()
        {
            PlayersList = new List<Player>();
            string[] documentLines = File.ReadAllLines(s_StoredPath);
            for (int i = 0; i < documentLines.Length; i++)
            {
                string[] playerData = documentLines[i].Split(';');
                PlayersList.Add(new Player(playerData[0]));
                for (int j = 1; j < playerData.Length; j++)PlayersList[i].Scores[j-1] = TimeSpan.Parse(playerData[j]);
            }
        }
        #endregion

        #region Methods
        /// <summary>This method is used to display the ranking.</summary>
        /// <returns>The ranking as a string.</returns>
        public override string ToString()
        {
            string text = "";
            for (int i = 0; i < PlayersList.Count; i++)
            {
                text += PlayersList[i].Name + " : ";
                for (int j = 0; j < PlayersList[i].Scores.Count; j++)text += PlayersList[i].Scores[j] + " ; ";
                text += "\n";
            }
            return text;
        }
        /// <summary>This method is used to display the leaderboard.</summary>
        public static void PrintLeaderBoard(Ranking ranking, string timeToString)
        {
            Methods.Title("-- LeaderBoard --",$"Your score has been recorded in the Labyrinth number {Board.s_Difficulty+1}!");
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
        }
        /// <summary>This method is used to sort the ranking according to a labyrinth difficulty.</summary>
        /// <returns>The ranking sorted as a string table.</returns>
        public string[] LeaderBoardSorting()
        {
            List<Player> playersListSorted = new List<Player>();
            for (int i = 0; i < PlayersList.Count; i++)if (PlayersList[i].Scores[Board.s_Difficulty] != TimeSpan.Zero)playersListSorted.Add(PlayersList[i]);
            playersListSorted.Sort((x, y) => x.Scores[Board.s_Difficulty].CompareTo(y.Scores[Board.s_Difficulty]));
            string[] rankingToString = new string[playersListSorted.Count];
            for (int i = 0; i < playersListSorted.Count; i++)
            { 
                if(i<9)rankingToString[i] = $"0{i+1} - {playersListSorted[i].Name} : {playersListSorted[i].Scores[Board.s_Difficulty]}";
                else rankingToString[i] = $"{i+1} - {playersListSorted[i].Name} : {playersListSorted[i].Scores[Board.s_Difficulty]}";
            }
            return rankingToString;
        }
        #endregion
    
    }
}
