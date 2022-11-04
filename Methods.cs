using System;
using static System.Console;
using static System.Environment;
using static System.Threading.Thread;
using static System.Convert;

namespace Labyrinth
{
    public abstract class Methods
    {
        #region Core Methods
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
                case 1 : GamePawn.symbol = Player.ChangeSymbol();break;
                case 2 : case -1: FinalExit();break;
                default: break;
            }
            LoadingScreen("-- Launching the game --");
        }
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
                    Title("-- Game session selection --","Please write the name of the session: ");
                    player.Name = ReadLine()+String.Empty;
                    if (player.Name.Length<21&&player.Name!=String.Empty)
                    {
                        while(player.Name.Length<20)
                        {
                            player.Name+=" ";
                            Player.s_SessionName = player.Name;
                        }
                    }
                }while (player.Name == "");
                
            }else
            {
                player.Name = ranking.PlayersList[position-1].Name;
                player.Scores = ranking.PlayersList[position-1].Scores;
                Player.CutName = player.Name;
            }
            ResetColor();
            Clear();
            return false;
        }
        #endregion

        #region Utility Methods
        public static void Pause()
        {
            WriteLine("\nPress [ENTER] to continue...");
            ReadLine();
        }
        public static void LoadingScreen(string text)
        {
            for (int i = 1; i < text.Length-1; i++)
            {
                Clear();
                WriteLine($"{text}\n");
                int t_interval = 2000/text.Length;
                ForegroundColor = ConsoleColor.DarkRed;
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
                ResetColor();
            }
            Clear();
        }
        public static void Title (string text,  string additionnalText = "",string specialText = "", int recurrence = 0)
        {
            Clear();
            if (recurrence != 0)
            {
                if (text != "")WriteLine($"\n{text}\n");
                if (specialText != "")WriteLine($"\n{specialText}\n");
                if (additionnalText != "")WriteLine($"\n{additionnalText}\n");
            }else
            {
                if (text != "")WriteLine($"\n{text}\n");
                if (specialText != "")WriteLine($"\n{specialText}\n");
                if (additionnalText != "") 
                {
                    WriteLine("");
                    for(int i = 0; i < additionnalText.Length; i++)
                    {
                        Write(additionnalText[i]);
                        Sleep(50);
                        if(KeyAvailable)
                        {
                            ConsoleKeyInfo keyPressed = ReadKey(true);
                            if(keyPressed.Key == ConsoleKey.Enter||keyPressed.Key == ConsoleKey.Escape)
                            {
                                Write(additionnalText.Substring(i+1));
                                break;
                            }
                        }
                    }
                    WriteLine("\n");
                }
            }
            
        }
        public static int ScrollingMenu (string[] choices, string text, string additionnalText = "", string specialText = "")
        {
            int position = 0;
            bool choiceMade = false;
            int recurrence = 0;
            while (!choiceMade)
            {
                Clear();
                Title(text,additionnalText,specialText,recurrence);
                string[]currentChoice = new string[choices.Length];
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == position)
                    {
                        currentChoice[i] = $" > {choices[i]}";
                        BackgroundColor = ConsoleColor.Green;
                        WriteLine(currentChoice[i]);
                        ResetColor();
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
        public static void FinalExit()
        {
            Clear();
            LoadingScreen("-- Shutting off --");
            Clear();
            Exit(0);
        }
        #endregion
    }
}