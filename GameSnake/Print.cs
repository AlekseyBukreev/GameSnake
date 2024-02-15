using System;

namespace GameSnake
{
    public struct Print
    {
        private const char Simbol = '█';

        public int X { get; private set; }

        public int Y { get; private set; }

        public int SimbolSizeX { get; private set; }

        public int SimbolSizeY { get; private set; }

        public ConsoleColor Color { get; private set; }

        public Print(int x, int y, ConsoleColor color, int simbolSizeX = 5, int simbolSizeY = 3)
        {
            X = x;
            Y = y;
            Color = color;
            SimbolSizeX = simbolSizeX;
            SimbolSizeY = simbolSizeY;
        }

        public void xyRemove(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Draw()
        {
            for (int i = 0; i < SimbolSizeX; i++)
            {
                for (int j = 0; j < SimbolSizeY; j++)
                {
                    Console.SetCursorPosition(X * SimbolSizeX + i, Y * SimbolSizeY + j);
                    Console.ForegroundColor = Color;
                    Console.WriteLine(Simbol);
                }
            }

        }
        public void Clear()
        {

            for (int i = 0; i < SimbolSizeX; i++)
            {
                for (int j = 0; j < SimbolSizeY; j++)
                {
                    Console.SetCursorPosition(X * SimbolSizeX + i, Y * SimbolSizeY + j);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(" ");
                }
            }

        }
    }
}
