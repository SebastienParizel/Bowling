using System;
using System.Text;
using Bowling.Builder;
using Bowling.Model;

namespace Bowling
{
    class Program
    {
        static void Main(string[] parameters)
        {
            try
            {
                IGameParser parser = GetGameParser();
                Game game = parser.BuildGame(parameters);

                DisplayFinalScore(game.GetScore());
                foreach (var frame in game.GetFrames())
                {
                    DisplayFrameContent(frame);
                }
            }catch(BowlingException e)
            {
                DisplayException(e);
            }
        }

        private static IGameParser GetGameParser()
        {
            IFrameBuilder buider = new FrameBuilder();
            return new GameParser(buider);
        }

        private static void DisplayFinalScore(int finalScore)
        {
            Console.WriteLine($"Final score: {finalScore}");
            Console.WriteLine("-----------------------");
        }

        private static void DisplayFrameContent(Frame frame)
        {
            string intermediateNewLine = $"{Environment.NewLine}\t";
            var message = new StringBuilder();
            message.Append($"* Frame n°{frame.FrameNumber}");
            message.Append(intermediateNewLine);
            message.Append($"intermediate score: { frame.CalculateScore()}");
            message.Append(intermediateNewLine);
            message.Append($"Is strike: { frame.IsStrike}");
            message.Append(intermediateNewLine);
            message.Append($"Is spare:{frame.IsSpare}");
            Console.WriteLine(message.ToString());
        }

        private static void DisplayException(BowlingException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.GetType().Name);
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
