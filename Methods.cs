using System;
using System.Diagnostics;
using static System.Console;
using static System.Environment;
using static System.Threading.Thread;
using static System.Convert;
using static System.IO.File;

namespace Labyrinth
{
    
    /// <summary> This class contains core methods for the Labyrinth game and utility methods for the whole program.</summary>
    abstract class Methods
    {
        #region Core Methods
        /// <summary> This method is used to set the console configuration. </summary>
        /// <param name="state"> Wether the config is used as the default config (true) or for the end of the program (false). </param>
        public static void ConsoleConfig(bool state = true)
        {
            if (state)
            {
                CursorVisible = false;
                BackgroundColor = ConsoleColor.Black;
                ForegroundColor = ConsoleColor.White;
            }
            else CursorVisible = true;
        }
        /// <summary> This method is used to display the main menu of the game.</summary>
        public static void MainMenu()
        {
            Clear();
            string title = @"▄▄▄█████▓ ██░ ██ ▓█████        ██▓    ▄▄▄       ▄▄▄▄ ▓██   ██▓ ██▀███   ██▓ ███▄    █ ▄▄▄█████▓ ██░ ██ 
▓  ██▒ ▓▒▓██░ ██▒▓█   ▀       ▓██▒   ▒████▄    ▓█████▄▒██  ██▒▓██ ▒ ██▒▓██▒ ██ ▀█   █ ▓  ██▒ ▓▒▓██░ ██▒
▒ ▓██░ ▒░▒██▀▀██░▒███         ▒██░   ▒██  ▀█▄  ▒██▒ ▄██▒██ ██░▓██ ░▄█ ▒▒██▒▓██  ▀█ ██▒▒ ▓██░ ▒░▒██▀▀██░
░ ▓██▓ ░ ░▓█ ░██ ▒▓█  ▄       ▒██░   ░██▄▄▄▄██ ▒██░█▀  ░ ▐██▓░▒██▀▀█▄  ░██░▓██▒  ▐▌██▒░ ▓██▓ ░ ░▓█ ░██ 
  ▒██▒ ░ ░▓█▒░██▓░▒████▒      ░██████▒▓█   ▓██▒░▓█  ▀█▓░ ██▒▓░░██▓ ▒██▒░██░▒██░   ▓██░  ▒██▒ ░ ░▓█▒░██▓
  ▒ ░░    ▒ ░░▒░▒░░ ▒░ ░      ░ ▒░▓  ░▒▒   ▓▒█░░▒▓███▀▒ ██▒▒▒ ░ ▒▓ ░▒▓░░▓  ░ ▒░   ▒ ▒   ▒ ░░    ▒ ░░▒░▒
    ░     ▒ ░▒░ ░ ░ ░  ░      ░ ░ ▒  ░ ▒   ▒▒ ░▒░▒   ░▓██ ░▒░   ░▒ ░ ▒░ ▒ ░░ ░░   ░ ▒░    ░     ▒ ░▒░ ░
  ░       ░  ░░ ░   ░           ░ ░    ░   ▒    ░    ░▒ ▒ ░░    ░░   ░  ▒ ░   ░   ░ ░   ░       ░  ░░ ░
          ░  ░  ░   ░  ░          ░  ░     ░  ░ ░     ░ ░        ░      ░           ░           ░  ░  ░
                                                     ░░ ░                                              ";
            switch(ScrollingMenu(new string[]{"Play      ","Options   ","Exit      "},"", "Welcome Adventurer! Use the arrow keys to move and press [ENTER] to confirm.",title))
            {
                case 1 : 
                    switch(ScrollingMenu(new string[]{"Change bonus symbol    ","See Labyrinth creation "},"", "Choose the feature you want to launch:",title))
                    {
                        case 0 : GamePawn.s_BonusSymbol = GamePawn.ChangeSymbol(); break;
                        case 1 : 
                            Board demonstration = new Board(0);
                            demonstration.LabyrinthCreationDemonstration();
                            break;
                        case -1 : MainMenu();break;
                    }
                    Pause();
                    MainMenu();
                    break;
                case 2 : case -1: FinalExit();break;
                default: break;
            }
            LoadingScreen("-- Launching the game --");
        }
        
        /// <summary>This method is used to display the rules of the game.</summary>
        public static void Rules()
        {
            string rules = @"                                             _______________________
   _______________________-------------------                       `\
 /:--__                                                              |
||< > |                                   ___________________________/
| \__/_________________-------------------                         |
|                                                                  |
 |                   The Labyrinth Game                             |
 |                                                                  |
 |           The Labyrinth game is a puzzle game                    |
  |           in which the player must find the way                  |
  |           the fastest to get out of the labyrinth.               |
  |           To do this, he must move through the maze              |
   |           using the ZQSD keys or the arrow keys.                 |
   |                                                                  |
   |           d : Represents the starting box.                       |
   |            a : Represents the arrival box.                      |
  |                                              ____________________|_
  |  ___________________-------------------------                      `\
  |/`--_                                                                 |
  ||[ ]||                                            ___________________/
   \===/___________________--------------------------";
            
            Title("-- Game Rules --", "",rules,1);
            Pause();

        }
        
        /// <summary>This method is used to define the current session.</summary>
        /// <param name= "ranking"> Display the choices among all of the sessions.</param>
        /// <param name= "player"> The player who is playing the game.</param>
        /// <returns> Wether or not the client chose to step back.</returns>
        public static bool DefineSession(Ranking ranking,Player player)
        {
            string[]sessionChoices = new string[ranking.PlayersList.Count+1];
            for(int i = 0; i < sessionChoices.Length; i++)
            {
                if(i == 0)sessionChoices[0]= "New player...       ";
                else sessionChoices[i]= ranking.PlayersList[i-1].Name;
            }
            int position = ScrollingMenu(sessionChoices, "-- Game session selection --","Please choose your game session wisely, or create one if yours is missing from the list:");
            if (position == -1)return true;
            else if (position == 0)
            {
                do
                {
                    Clear();
                    Title("-- Game session selection --","Please write a new session name: ");
                    player.Name = ReadLine()+String.Empty;
                    if (player.Name.Length<21&&player.Name!=String.Empty)
                    {
                        while(player.Name.Length<20)
                        {
                            player.Name+=" ";
                        }
                        if(!player.IsNewPlayer())
                        {
                            WriteLine("\nThis session already exists, please choose another name.");
                            player.Name = String.Empty;
                            Pause();
                        }
                    }
                }while (player.Name == "");
                Player.CutName = player.Name;
            }else
            {
                player.Name = ranking.PlayersList[position-1].Name;
                player.Scores = ranking.PlayersList[position-1].Scores;
                Player.CutName = player.Name;
            }
            ConsoleConfig();
            Clear();
            return false;
        }

        /// <summary>This method is used to apply bonuses.</summary>
        /// <param name="time">The player's time before applying bonuses.</param>
        /// <returns>the time parameter changed after applying bonus that have been taken from the game.</returns>
        public static TimeSpan BonusAttribution(TimeSpan time)
        {
            if (GamePawn.s_BonusTaken)
            {
                switch (Board.s_Difficulty)
                {
                    case 0 : time = time.Subtract(new TimeSpan(0,0,0,0,150));break;
                    case 1 : time = time.Subtract(new TimeSpan(0,0,0,2,0));break;
                    case 2 : time = time.Subtract(new TimeSpan(0,0,0,10,0));break;
                    case 3 : time = time.Subtract(new TimeSpan(0,0,0,30,0));break;
                }
            } 
            return time;
        }
        /// <summary>This method is used to display the ranking of the current session.</summary>
        /// <param name="player">The current player.</param>
        /// <param name="timer">The current time the player has taken.</param>
        /// <param name="ranking">The ranking of the labyrinth.</param>
        public static void LeaderBoardCreation(Player player, Stopwatch timer, Ranking ranking)
        {
            TimeSpan time = timer.Elapsed;
            time = BonusAttribution(time);
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
            Ranking.PrintLeaderBoard(ranking, timeToString);
        }
        #endregion

        #region Utility Methods
        /// <summary>This method is used to pause the program.</summary>
        public static void Pause()
        {
            WriteLine("\nPress [ENTER] to continue...");
            ReadLine();
        }
        
        /// <summary>This method is used to display a loading screen.</summary>
        public static void LoadingScreen(string text)
        {
            for (int i = 1; i < text.Length-1; i++)
            {
                Clear();
                WriteLine($"{text}\n");
                int t_interval = 2000/text.Length;
                ForegroundColor = ConsoleColor.Red;
                char[]loadingBar = new char[text.Length];
                for (int k = 0; k <= loadingBar.Length-1; k++)
                {
                    if (k == 0)loadingBar[0] = '[';
                    else if(k <=i)loadingBar[k]='-';
                    else if (k == loadingBar.Length-1)loadingBar[k] = ']';
                    else loadingBar[k]= ' ';
                }
                if (i == loadingBar.Length-2)
                {
                    ForegroundColor = ConsoleColor.Green;
                    for (int l = 0; l < loadingBar.Length; l++)Write(loadingBar[l]);
                    decimal percentage = (ToDecimal(i+2)/ToDecimal(text.Length))*100;
                    Write($" {(int)percentage} %\n");
                    Sleep(800);
                }
                else
                {
                    for (int l = 0; l < loadingBar.Length; l++)Write(loadingBar[l]);
                    decimal percentage = (ToDecimal(i+2)/ToDecimal(text.Length))*100;
                    Write($" {(int)percentage} %\n");
                    Sleep(t_interval);
                }
                ConsoleConfig();
            }
            Clear();
        }
        
        /// <summary>This method is used to display a title.</summary>
        /// <param name= "text"> The content of the title.</param>
        /// <param name= "additionalText"> The subtitle of the title.</param>
        /// <param name= "specialText"> Special text as a font.</param>
        /// <param name= "recurrence"> Whether the title has been displayed yet or not.</param>
        public static void Title (string text,  string additionalText = "",string specialText = "", int recurrence = 0)
        {
            Clear();
            if (recurrence != 0)
            {
                if (text != "")WriteLine($"\n{text}\n");
                if (specialText != "")WriteLine($"\n{specialText}\n");
                if (additionalText != "")WriteLine($"\n{additionalText}\n");
            }else
            {
                if (text != "")WriteLine($"\n{text}\n");
                if (specialText != "")WriteLine($"\n{specialText}\n");
                if (additionalText != "") 
                {
                    WriteLine("");
                    for(int i = 0; i < additionalText.Length; i++)
                    {
                        Write(additionalText[i]);
                        Sleep(50);
                        if(KeyAvailable)
                        {
                            ConsoleKeyInfo keyPressed = ReadKey(true);
                            if(keyPressed.Key == ConsoleKey.Enter||keyPressed.Key == ConsoleKey.Escape)
                            {
                                Write(additionalText.Substring(i+1));
                                break;
                            }
                        }
                    }
                    WriteLine("\n");
                }
            }
            
        }
        
        /// <summary>This method is used to display a scrolling menu.</summary>
        /// <param name= "choices"> The choices of the menu.</param>
        /// <param name= "text"> The content of the title.</param>
        /// <param name= "additionalText"> The subtitle of the title.</param>
        /// <param name= "specialText"> Special text as a font.</param>
        /// <returns> The position of the choice selected.</returns>
        public static int ScrollingMenu (string[] choices, string text, string additionalText = "", string specialText = "")
        {
            int position = 0;
            bool choiceMade = false;
            int recurrence = 0;
            while (!choiceMade)
            {
                Clear();
                Title(text,additionalText,specialText,recurrence);
                string[]currentChoice = new string[choices.Length];
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == position)
                    {
                        currentChoice[i] = $" > {choices[i]}";
                        BackgroundColor = ConsoleColor.Green;
                        ForegroundColor = ConsoleColor.Black;
                        WriteLine(currentChoice[i]);
                        ConsoleConfig();
                    }
                    else 
                    {
                        
                        currentChoice [i]= $"   {choices[i]}";
                        WriteLine(currentChoice[i]);
                    }
                }
                switch(ReadKey().Key)
                {
                    case ConsoleKey.UpArrow : case ConsoleKey.Z : if(position == 0)position = choices.Length-1; else if(position > 0) position--;break;
                    case ConsoleKey.DownArrow : case ConsoleKey.S : if(position == choices.Length-1)position = 0; else if(position < choices.Length-1)position++;break;
                    case ConsoleKey.Enter : return position;
                    case ConsoleKey.Escape : return -1;
                }
                recurrence++;
            }
            return position;
        }
        
        /// <summary>This method is used to exit the game.</summary>
        public static void FinalExit()
        {
            Clear();
            LoadingScreen("-- Shutting off --");
            Clear();
            ConsoleConfig(false);
            Exit(0);
        }
        #endregion
    }
}
