using System;
using static System.Console;

namespace Labyrinth
{
    public class Player
    {
        #region Attributes
        public string Name {get; set;}
        public List <TimeSpan> Scores {get; set;}
        public static string? s_SessionName {get; set;}
        #endregion
        
        #region Constructor
        public Player(string name = "")
        {
            Name = name;
            Scores = new List<TimeSpan>(){TimeSpan.Zero,TimeSpan.Zero,TimeSpan.Zero,TimeSpan.Zero};
        }
        #endregion
        
        #region Properties
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
        public override string ToString()
        {
            string text = Name;
            for (int i = 0; i < Scores.Count; i++)
            {
                text += ";" + Scores[i] ;
            }
            return text;
        }
        public bool IsNewPlayer()
        {
            string[] documentLines = File.ReadAllLines(Ranking.s_StoragePath);
            for (int i = 0; i < documentLines.Length; i++)
            {
                string[] playerData = documentLines[i].Split(';');
                if (playerData[0] == Name)return false;
            }
            return true;
        }
        public int IndexOfPlayer()
        {
            string[] documentLines = File.ReadAllLines(Ranking.s_StoragePath);
            for (int i = 0; i < documentLines.Length; i++)
            {
                string[] playerData = documentLines[i].Split(';');
                if (playerData[0] == Name)return i;
            }
            return -1;
        }
        public static char ChangeSymbol ()
        {
            if(Methods.ScrollingMenu(new string[]{"Oui","Non"},"-- Options --","Le pion par défaut est le π, souhaitez-vous le changer ?")==0)
            {
                Write("\nVeuillez saisir le caractère que vous souhaitez utiliser : ");
                return ReadKey().KeyChar;
            }
            return 'π';
        }
        #endregion
  
    }
}
