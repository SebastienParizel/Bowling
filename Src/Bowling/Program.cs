using System;
using System.Diagnostics;
using System.Text;
using Bowling.Builder;
using Bowling.Model;

namespace Bowling
{
    class Program
    {
        private const int ApplicationEventId = 400;

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
            }
            catch (BowlingException e)
            {
                DisplayException(e);
            }
            catch (Exception e)
            {
                LogToEvenViewer(e);
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
            message.Append($"Intermediate score:\t{ frame.CalculateScore()}");
            message.Append(intermediateNewLine);
            message.Append($"Is strike:\t{ frame.IsStrike}");
            message.Append(intermediateNewLine);
            message.Append($"Is spare:\t{frame.IsSpare}");
            message.Append(intermediateNewLine);
            message.Append($"First launch:\t{frame.FirstLaunch}");
            message.Append(intermediateNewLine);
            message.Append($"Second launch:\t{frame.SecondLaunch}");
            Console.WriteLine(message.ToString());
        }

        private static void DisplayException(BowlingException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        private static void LogToEvenViewer(Exception e)
        {
            StringBuilder message=null;
            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Bowling";
                    message = new StringBuilder($"An exception occured of type { e.GetType().Name}");
                    message.Append(e.Message);
                    message.Append(e.StackTrace);

                    eventLog.WriteEntry(message.ToString(), EventLogEntryType.Error, ApplicationEventId);
                }
                Console.WriteLine("An error occured");
            }
            catch (Exception)
            {
                Console.WriteLine(message.ToString());
            }
            Console.ResetColor();
        }
    }
}
