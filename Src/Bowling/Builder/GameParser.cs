using System;
using System.Collections.Generic;
using System.Linq;
using Bowling.Model;

namespace Bowling.Builder
{
    public class GameParser : IGameParser
    {
        private readonly IFrameBuilder _builder;
        private int _index;

        public GameParser(IFrameBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _index = 0;
        }


        public Game BuildGame(string[] parameters)
        {
            var game = new Game();
            while (_index < parameters.Length)
            {
                var firstLaunch = GetNextParameter(parameters);
                if (IsStrikeValue(firstLaunch))
                {
                    var strike = BuildStrike();
                    game.AddFrame(strike);
                }
                else
                {
                    string secondLaunch = GetNextParameter(parameters);
                    if (IsSpareValue(secondLaunch))
                    {
                        var spare = BuildSpare(firstLaunch);
                        game.AddFrame(spare);
                    }
                    else
                    {
                        var frame = BuildFrame(firstLaunch, secondLaunch);
                        game.AddFrame(frame);
                    }
                }
            }
            return game;
        }

        private string GetNextParameter(string[] parameters)
        {
            if (_index < parameters.Length)
            {
                string param = parameters[_index];
                _index++;
                return param;
            }
            return "0";
        }

        private Frame BuildFrame(string firstLaunch, string secondLaunch)
        {
            var first = GetLaunchValue(firstLaunch);
            var second = GetLaunchValue(secondLaunch);
            return _builder.CreateFrame(first, second);
        }

        private Frame BuildSpare(string firstLaunch)
        {
            int launch = GetLaunchValue(firstLaunch);
            return _builder.CreateSpare(launch);
        }

        private Frame BuildStrike()
        {
            return _builder.CreateStrike();
        }

        private int GetLaunchValue(string param)
        {
            int launchValue;
            if (IsStrikeValue(param))
                return 10;
            if (IsZeroValue(param))
                return 0;
            if (IsSpareValue(param))
                throw new BowlingException("A spare can only be done on the second launch of the frame");
            if (!Int32.TryParse(param, out launchValue))
                throw new BowlingException($"Not supported value [{param}]. Please use one of the following value: [0,1,2,3,4,5,6,7,8,9,10,-,/,X]");
            return launchValue;
        }

        private bool IsStrikeValue(string param)
        {
            return param.Equals("x", StringComparison.InvariantCultureIgnoreCase) || param.Equals("10", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsSpareValue(string param)
        {
            return param.Equals("/", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsZeroValue(string param)
        {
            return param.Equals("-", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
