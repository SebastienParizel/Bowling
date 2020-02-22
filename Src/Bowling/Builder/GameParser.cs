using System;
using System.Collections.Generic;
using System.Linq;
using Bowling.Model;

namespace Bowling.Builder
{
    public class GameParser : IGameParser
    {
        private readonly IFrameBuilder _builder;

        public GameParser(IFrameBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }


        public Game BuildGame(string[] parameters)
        {
            if (!IsEvenNumberOfParameter(parameters))
                throw new BowlingException("Parameters must be even");
            var game = new Game();
            for(int i=0; i<parameters.Length; i += 2)
            {
                var frame = BuildFrame(parameters[i], parameters[i + 1]);
                game.AddFrame(frame);
            }

            return game;
        }

        private Frame BuildFrame(string first, string second)
        {
            if (IsStrikeValue(first))
                return _builder.CreateStrike();
            var firstLaunch = GetLaunchValue(first);
            if (IsSpareValue(second))
                return _builder.CreateSpare(firstLaunch);
            var secondLaunch = GetLaunchValue(second);
            return _builder.CreateFrame(firstLaunch, secondLaunch);
        }

        private bool IsEvenNumberOfParameter(string [] paramaters)
        {
            return paramaters.Length % 2 == 0;
        }

        private int GetLaunchValue(string param)
        {
            int launchValue;
            if (IsStrikeValue(param))
                return 10;
            if (IsZeroValue(param))
                return 0;
            if (IsSpareValue(param))
                return 0;
            if (!Int32.TryParse(param, out launchValue))
                throw new BowlingException($"Not supported value [{param}]. Please use one of the following value: [0,1,2,3,4,5,6,7,8,9,10,-,/,X]");
            return launchValue;
        }

        private bool IsStrikeValue(string param)
        {
            return param.Equals("x", StringComparison.InvariantCultureIgnoreCase);
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
