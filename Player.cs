using System;
using static System.Console;

namespace Labyrinth
{
    /// <summary>The player class</summary>
    public class Player
    {
        #region Attributes
        /// <summary>The name of the player.</summary>
        public string Name {get; set;}
        
        /// <summary>The scores of the player in the different labyrinths.</summary>
        public List <TimeSpan> Scores {get; set;}

        /// <summary>The current session the player is in.</summary>
        public static string? s_SessionName {get; set;}
        #endregion
        
        #region Constructor
        /// <summary>This method is used to initialize a new instance of the <see cref="T:Labyrinth.Player"/> class.</summary>
        /// <param name="name">The name of the player is optional. You can either set it from the instance, or define it later on.</param>
        public Player(string name = "")
        {
            Name = name;
            Scores = new List<TimeSpan>(){TimeSpan.Zero,TimeSpan.Zero,TimeSpan.Zero,TimeSpan.Zero};
        }
        #endregion
        
        #region Properties
        /// <summary>This property is used to get the name of the player whithout the visual spaces.</summary>
        public static string CutName
        {
            set
            {
                s_SessionName = value;
                while(s_SessionName[s_SessionName.Length-1] == ' ')s_SessionName = s_SessionName.Substring(0,s_SessionName.Length-1);
            }
        }
        #endregion

        #region Utility Methods
        /// <summary>This method is used to display the scores of the player.</summary>
        public override string ToString()
        {
            string text = Name;
            for (int i = 0; i < Scores.Count; i++)
            {
                text += ";" + Scores[i] ;
            }
            return text;
        }
        /// <summary>This method is used to check in the list of player if the player is already in it.</summary>
        /// <returns>Wether the player is in the list or not.</returns>
        public bool IsNewPlayer()
        {
            string[] documentLines = File.ReadAllLines(Ranking.s_StoredPath);
            for (int i = 0; i < documentLines.Length; i++)
            {
                string[] playerData = documentLines[i].Split(';');
                if (playerData[0] == Name)return false;
            }
            return true;
        }
        /// <summary>This method is used to get the index of the player in the list.</summary>
        /// <returns>The index of the player in the list.</returns>
        public int IndexOfPlayer()
        {
            string[] documentLines = File.ReadAllLines(Ranking.s_StoredPath);
            for (int i = 0; i < documentLines.Length; i++)
            {
                string[] playerData = documentLines[i].Split(';');
                if (playerData[0] == Name)return i;
            }
            return -1;
        }
        /// <summary>This method is used to change the pawn symbol.</summary>
        /// <returns>The symbol of the pawn as a char.</returns>
        public static char ChangeSymbol ()
        {
            if(Methods.ScrollingMenu(new string[]{"Yes ","No "},"-- Options --","The default pawn is π, would you like to change it?")==0)
            {
                Write("\nPlease type your replacement character: ");
                return ReadKey().KeyChar;
            }
            return 'π';
        }
        #endregion
  
    }
}
