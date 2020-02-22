using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Builder;
using Bowling.Model;

namespace Bowling
{
    class Program
    {
        static void Main(string[] parameters)
        {
            IFrameBuilder buider = new FrameBuilder();
            GameParser parser = new GameParser(buider);
            Game game = parser.BuildGame(parameters);
            Console.WriteLine($"Final score: {game.GetScore()}");
            Console.WriteLine("-----------------------");
            foreach (var frame in game.GetFrames())
            {
                DisplayFrameContent(frame);
            }
            Console.ReadKey();
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
    }
}
