using System;
using static System.Console;

namespace Labyrinth
{
    /// <summary>The board class</summary>
    class Board
    {
        #region Attributes
        /// <summary>The starting position.</summary>
        public Position Start {get; private set;}
        /// <summary>The ending position.</summary>
        public Position End {get; private set;}
        /// <summary>The board as a matrix.</summary>
        public int[,] Matrix {get; set; }
        /// <summary>The difficulty of the labyrinth.</summary>
        public static int s_Difficulty {get; set; }
        #endregion

        #region Constructor
        /// <summary>This method is used to initialize a new instance of the <see cref="T:Labyrinth.Board"/> class.</summary>
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
        /// <summary>This method is used to generate the matrix representing the board.</summary>
        /// <returns>The generated matrix.</returns>
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
        /// <summary>This method is used to fill the matrix with increments so that every empty space is filled with a different value.</summary>
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
        /// <summary>This method is used to create a bridge randomly between two lines or two columns.If the values between thses two is different, it breaks the wall and enable to generate the labyrinth step by step.</summary>
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
        /// <summary>This method is used to replace the values int the matrix after breaking a wall. This allows the matrix to finally contain exclusively 1(walls) or a value(the paths). This algorithm provides a simple labyrinth where every position in a path is accessible from another.</summary>
        /// <param name="previous">The value to replace.</param>
        /// <param name="next">The value to set.</param>
        private void SwitchValues(int previous, int next)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)for (int j = 0; j < Matrix.GetLength(1); j++)if (Matrix[i,j]==previous)Matrix[i,j]=next;
        }
        /// <summary>This method is used to check if the matrix is completed.</summary>
        /// <returns>True if the matrix is completed, false otherwise.</returns>
        private bool IsMatrixDone()
        {
            int referenceValue = Matrix[1,1];
            for (int i = 1; i < Matrix.GetLength(0)-1; i++)for (int j = 1; j < Matrix.GetLength(1)-1; j++)if (Matrix[i, j] != referenceValue && Matrix[i, j] != 1) return false;
            return true;
        }
        /// <summary>This method is used to display the matrix in the console.</summary>
        public void PrintBoard()
        {
            Clear();
            for(int i = 0; i < Matrix.GetLength(0) ; i++)
            {
                BackgroundColor = ConsoleColor.Black;
                ForegroundColor = ConsoleColor.Black;
                string line = "";
                for (int j = 0; j < Matrix.GetLength(0); j++)
                {
                    switch(Matrix[i,j])
                    {
                        case 0 : line += "  ";break;
                        case 1 : line += "# ";break;
                        case 2 : line += "  ";continue;
                        case 3 : line += "  ";continue;
                        case 4 : line += ". ";continue;
                        case 5 : line += GamePawn.symbol+" ";continue; 
                    }
                }
                if (i == 1)
                {
                    BackgroundColor = ConsoleColor.Red;
                    ForegroundColor = ConsoleColor.Black;
                    Write("Start → ");
                    BackgroundColor = ConsoleColor.Green;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine(line);
                    
                }
                else if (i == Matrix.GetLength(0)-2)
                {
                    Write("\t");
                    BackgroundColor = ConsoleColor.Green;
                    Write(line);
                    BackgroundColor = ConsoleColor.Blue;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine(" → Exit");
                }       
                else 
                {
                    Write("\t");
                    BackgroundColor = ConsoleColor.Green;
                    WriteLine(line);
                }
            }
            Methods.ConsoleConfig();
        }
        /// <summary>This method is used to check if a position is available in the matrix.</summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the position is available, false otherwise.</returns>
        public bool IsAvailable(Position position) => (Matrix[position.X,position.Y] == 1 || Matrix[position.X,position.Y] == 5)? false : true;
        #endregion
        
    
    }
}
