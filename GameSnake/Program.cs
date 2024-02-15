using System;
using System.Diagnostics;
using static System.Console;

namespace GameSnake
{
    class Program
    {
        private const int widthMap = 15;
        private const int heightMap = 15;

        private static int Frame = 300;

        private const int maxWidthMap = widthMap * 6; // Умножаем не на 5, а на 6 для приятного зрению вида консоли
        private const int maxHeightMap = 50; // Умножаем на 3 и добавляем 5 для более приятного зрению вида консоли

        private static Random rand = new Random();
        private static byte oldScore = 0;

        private const ConsoleColor mapColor = ConsoleColor.White;
                
        static void Main()
        {
            SetWindowSize(maxWidthMap, maxHeightMap);
            SetBufferSize(maxWidthMap, maxHeightMap);

            CursorVisible = false;

            bool OpenGame = true;
                        
            while (OpenGame)
            {
                StartGame();

                Thread.Sleep(100);

                ConsoleKeyInfo chekKey = Console.ReadKey();

                switch (chekKey.Key)
                {
                    case ConsoleKey.Escape:
                        OpenGame = false;
                        Clear();
                        break;
                    default:
                        OpenGame = true;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        SetCursorPosition(5, 1);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Лучший рекорд: {oldScore}");
                        break;
                }



            }
        }
        static void StartGame()
        {
            byte Score = 0; // поле 15х15 = 225 меньше 255

            Clear();

            DrawMap();

            Direction MoveDirection = Direction.Right;

            Snake snake = new Snake(9, 8);

            Print eat = Eat(snake);
            eat.Draw();

            SetCursorPosition(37, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Текущий счет: {Score}");
            SetCursorPosition(5, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Лучший рекорд: {oldScore}");

            Stopwatch stopwatch = new Stopwatch();

            while (true)
            {
                stopwatch.Restart();

                Direction chekMovement = MoveDirection;

                while (stopwatch.ElapsedMilliseconds <= Frame)
                {
                    if (chekMovement == MoveDirection)
                    {
                        MoveDirection = ReadMove(MoveDirection);
                    }
                }

                if (snake.HeadSnake.X == eat.X && snake.HeadSnake.Y == eat.Y)
                {
                    snake.Move(MoveDirection, false);
                    eat = Eat(snake);
                    eat.Draw();
                    Score++;
                    SetCursorPosition(37, 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Текущий счет: {Score}");
                    DrawMap();
                }
                else
                {
                    snake.Move(MoveDirection, true);
                }


                if (snake.BodySnake.Any(i => i.X == snake.HeadSnake.X && i.Y == snake.HeadSnake.Y))
                {
                    break;
                }

                ChekingDifficultyLevel(Score);

                if (Score >= 220)
                {
                    break;
                }
            }

            if (Score > oldScore)
            {
                oldScore = Score;
            }

            snake.DeleteSnake();
            eat.Clear();
            Frame = 300;
            SetCursorPosition(38, 22);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Конец игры!");
            SetCursorPosition(38, 23);
            Console.Write($"Ваш счет: {Score}");
            if (Score >= 220)
            {
                SetCursorPosition(36, 24);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Вы победитель!");
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            SetCursorPosition(17, 25);
            Console.Write("Чтобы начать заново, нажмите любую клавишу кроме ESC");
            SetCursorPosition(27, 26);
            Console.Write("Чтобы выйти из игры, нажмите ESC");
        }

        static Direction ReadMove(Direction direction)
        {
            if (!KeyAvailable)
                return direction;

            ConsoleKey key = ReadKey(true).Key;

            direction = key switch
            {
                ConsoleKey.UpArrow when direction != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when direction != Direction.Up => Direction.Down,
                ConsoleKey.RightArrow when direction != Direction.Left => Direction.Right,
                ConsoleKey.LeftArrow when direction != Direction.Right => Direction.Left,
                _ => direction
            };

            return direction;
        }

        static void DrawMap()
        {
            for (int i = 0; i < widthMap; i++)
            {
                new Print(i + 1, 1, mapColor).Draw();
                new Print(i + 1, heightMap - 1, mapColor).Draw();
            }

            for (int i = 0; i < heightMap - 1; i++)
            {
                new Print(1, i + 1, mapColor).Draw();
                new Print(widthMap, i + 1, mapColor).Draw();
            }

        }

        static Print Eat(Snake snake)
        {
            Print eat;

            do
            {
                eat = new Print(rand.Next(2, widthMap - 2), rand.Next(2, heightMap - 2), ConsoleColor.DarkGreen);
            } while (snake.HeadSnake.X == eat.X && snake.HeadSnake.Y == eat.Y || snake.BodySnake.Any(i => i.X == eat.X && i.Y == eat.Y));

            return eat;
        }

        static void ChekingDifficultyLevel(byte score)
        {
            short StepLevel = 15;
            short StartFrame = 300;
            short StepFrame = 50;

            for (int level = 4; level >= 0; level--)
            {
                if (score >= StepLevel * level)
                {
                    Frame = StartFrame - StepFrame * level;
                    break;
                }
            }
        }
    }
}