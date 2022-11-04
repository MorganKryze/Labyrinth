using System;
using System.IO;
using static System.Environment;

namespace Labyrinth
{
    public class Ranking
    {
        
        #region Attributes
        public List<Player> PlayersList {get; set;}
        public static string s_StoragePath = "Storage.txt";
        #endregion

        #region Constructor
        public Ranking()
        {
            PlayersList = new List<Player>();
            string[] documentLines = File.ReadAllLines(s_StoragePath);
            for (int i = 0; i < documentLines.Length; i++)
            {
                string[] playerData = documentLines[i].Split(';');
                PlayersList.Add(new Player(playerData[0]));
                for (int j = 1; j < playerData.Length; j++)PlayersList[i].Scores[j-1] = TimeSpan.Parse(playerData[j]);
            }
        }
        #endregion

        #region Methods
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
