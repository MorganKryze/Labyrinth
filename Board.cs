using System;
using static System.Console;

namespace Labyrinth
{
    public class Board
    {
        #region Attributes
        public Position Start {get; private set;}
        public Position End {get; private set;}
        public int[,] Matrix {get; set; }
        public static int s_Difficulty {get; set; }
        #endregion

        #region Constructor
        public Board()
        {
            Start = new Position(0,0);
            End = new Position(0,0);
            switch (s_Difficulty)
            {
                case 0 : Matrix = new int[5,5]; break;
                case 1 : Matrix = new int[15,15]; break;
                case 2 : Matrix = new int[35,35]; break;
                case 3 : Matrix = new int[55,55]; break;
                default : Matrix = new int[5,5]; break;
            }
            Matrix = MatrixGeneration();
        }
        #endregion

        #region Methods
        private int[,] MatrixGeneration()
        {
            int size = Matrix.GetLength(0);
            for(int i = 0; i < size; i+=2)for(int j = 0; j < size; j++)Matrix[i, j] = 1;
            for(int i = 0; i < size; i++)for(int j = 0; j < size; j+=2)Matrix[i, j] = 1;
            FillWithIncrements();
            Bridge();
            SwitchValues(Matrix[1, 1], 0);
            Matrix[1, 0] = 2;
            Start = new Position(1,0);
            Matrix[size - 2, size - 1]=3;
            End = new Position(size - 2, size - 1);
            return Matrix;
        }
        private void FillWithIncrements()
        {
            int increment = 2;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 1; j < Matrix.GetLength(0)-1; j++ , increment++)
                {
                    if (Matrix[i,j]==0)
                    {
                        Matrix[i,j]=increment;
                        
                    }
                }
            }
        }
        private void Bridge()
        {
            List<Position> positions = new List<Position>();
            Random rnd = new Random();
            bool cond = true;
            int size = Matrix.GetLength(0);
            while(!IsMatrixDone())
            {
                Position p = new Position(rnd.Next(1, size - 1), rnd.Next(1, size - 1));
                
                if (cond&&!positions.Contains(p))
                {
                    positions.Add(p);
                    if (Matrix[p.X,p.Y]==1&&Matrix[p.X,p.Y+1]!=1&&Matrix[p.X,p.Y-1]!=1)
                    {
                        if (Matrix[p.X,p.Y-1]>Matrix[p.X,p.Y+1])
                        {
                            SwitchValues(Matrix[p.X,p.Y+1],Matrix[p.X,p.Y-1]);
                            Matrix[p.X,p.Y]=Matrix[p.X,p.Y-1];
                    
                        }
                        else if (Matrix[p.X,p.Y-1]<Matrix[p.X,p.Y+1])
                        {
                            SwitchValues(Matrix[p.X,p.Y-1],Matrix[p.X,p.Y+1]);
                            Matrix[p.X,p.Y]=Matrix[p.X,p.Y+1];
                        }
                    }
                    cond = false;
                }
                else if (!positions.Contains(p))
                {
                    positions.Add(p);
                    if (Matrix[p.X,p.Y]==1&&Matrix[p.X+1,p.Y]!=1&&Matrix[p.X-1,p.Y]!=1)
                    {
                        if (Matrix[p.X-1,p.Y]>Matrix[p.X+1,p.Y])
                        {
                            SwitchValues(Matrix[p.X+1,p.Y],Matrix[p.X-1,p.Y]);
                            Matrix[p.X,p.Y]=Matrix[p.X-1,p.Y];
                        }
                        else if (Matrix[p.X-1,p.Y]<Matrix[p.X+1,p.Y])
                        {
                            SwitchValues(Matrix[p.X-1,p.Y],Matrix[p.X+1,p.Y]);
                            Matrix[p.X,p.Y]=Matrix[p.X+1,p.Y];
                        }
                    }
                    cond = true;
                }
            }
            SwitchValues(Matrix[1, 1], 0);
        }
        private void SwitchValues(int previous, int next)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)for (int j = 0; j < Matrix.GetLength(1); j++)if (Matrix[i,j]==previous)Matrix[i,j]=next;
        }
        private bool IsMatrixDone()
        {
            int referenceValue = Matrix[1,1];
            for (int i = 1; i < Matrix.GetLength(0)-1; i++)for (int j = 1; j < Matrix.GetLength(1)-1; j++)if (Matrix[i, j] != referenceValue && Matrix[i, j] != 1) return false;
            return true;
        }
        public void PrintBoard()
        {
            Clear();
            for(int i = 0; i < Matrix.GetLength(0) ; i++)
            {
                BackgroundColor = ConsoleColor.Green;
                ForegroundColor = ConsoleColor.Black;
                if (i == 1||i == Matrix.GetLength(0)-2)
                {
                    for (int j = 0; j < Matrix.GetLength(0); j++)
                    {
                    switch(Matrix[i,j])
                    {
                        case 0 : Write(" ");break;
                        case 1 : Write("#");break;
                        case 2 : BackgroundColor = ConsoleColor.DarkRed;ForegroundColor = ConsoleColor.White;Write("d");break;
                        case 3 : BackgroundColor = ConsoleColor.DarkBlue;ForegroundColor = ConsoleColor.White;Write("a");break;
                        case 4 : Write(".");break;
                        case 5 : Write(GamePawn.symbol);break; 
                        default : break;
                    }
                    BackgroundColor = ConsoleColor.Green;
                    ForegroundColor = ConsoleColor.Black;
                    Write(" ");
                    }
                    WriteLine();
                }else 
                {
                    string retour = "";
                    for (int j = 0; j < Matrix.GetLength(0); j++)
                    {
                        switch(Matrix[i,j])
                        {
                            case 0 : retour += ' ';break;
                            case 1 : retour += '#';break;
                            case 2 : retour += 'd';break;
                            case 3 : retour += 'a';break;
                            case 4 : retour += '.';break;
                            case 5 : retour += GamePawn.symbol;break; 
                            default : break;
                        }
                        retour+= " ";
                    }
                    WriteLine(retour);
                }
            }
            ResetColor();
        }
        public bool IsAvailable(Position position) => (Matrix[position.X,position.Y] == 1 || Matrix[position.X,position.Y] == 5)? false : true;
        #endregion
        
    
    }
}
