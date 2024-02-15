using System;

namespace GameSnake
{
    public class Snake
    {
        public ConsoleColor ColorHead { get; private set; }
        public ConsoleColor ColorBody { get; private set; }

        public Print HeadSnake { get; private set; }

        public List<Print> BodySnake { get; private set; } = new List<Print>();

        public Snake(int xHead, int yHead, int bodyLength = 2, ConsoleColor colorHead = ConsoleColor.Magenta, ConsoleColor colorBody = ConsoleColor.Blue)
        {
            ColorHead = colorHead;
            ColorBody = colorBody;

            HeadSnake = new Print(xHead, yHead, ColorHead);

            for (int i = 0; i < bodyLength; i++)
            {
                BodySnake.Add(new Print(HeadSnake.X - i - 1, yHead, ColorBody));
                                                                                
            }

            DrawSnake();
        }

        public void DrawSnake()
        {
            HeadSnake.Draw();

            foreach (Print print in BodySnake)
            {
                print.Draw();
            }
        }

        public void ClearSnake()
        {
            HeadSnake.Clear();

            BodySnake[BodySnake.Count - 1].Clear();
        }

        public void DeleteSnake()
        {
            HeadSnake.Clear();

            foreach (Print print in BodySnake)
            {
                print.Clear();
            }
        }

        public void Move(Direction direction, bool chekEat = false)
        {
            ClearSnake();

            BodySnake.Insert(0, new Print(HeadSnake.X, HeadSnake.Y, ColorBody));

            if (chekEat == true)
            {
                BodySnake.RemoveAt(BodySnake.Count - 1);
            }

            HeadSnake = direction switch
            {
                Direction.Up when HeadSnake.Y != 2 => new Print(HeadSnake.X, HeadSnake.Y - 1, ColorHead),
                Direction.Up when HeadSnake.Y == 2 => new Print(HeadSnake.X, 13, ColorHead),
                Direction.Down when HeadSnake.Y != 13 => new Print(HeadSnake.X, HeadSnake.Y + 1, ColorHead),
                Direction.Down when HeadSnake.Y == 13 => new Print(HeadSnake.X, 2, ColorHead),
                Direction.Right when HeadSnake.X != 14 => new Print(HeadSnake.X + 1, HeadSnake.Y, ColorHead),
                Direction.Right when HeadSnake.X == 14 => new Print(2, HeadSnake.Y, ColorHead),
                Direction.Left when HeadSnake.X != 2 => new Print(HeadSnake.X - 1, HeadSnake.Y, ColorHead),
                Direction.Left when HeadSnake.X == 2 => new Print(14, HeadSnake.Y, ColorHead),
                _ => HeadSnake
            };

            DrawSnake();
        }
    }
}
