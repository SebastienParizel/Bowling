using System;
using System.Collections.Generic;
using System.Linq;
using Bowling.Model;

namespace Bowling.Builder
{
    public class GameParser
    {
        private readonly Queue<int> _launches;

        public GameParser(string[] parameters)
        {
            _launches = new Queue<int>();
            InitializeLaunches(parameters);
        }

        private void InitializeLaunches(string[] parameters)
        {
            foreach (var param in parameters)
            {
                int launch;
                if (!Int32.TryParse(param, out launch))
                {
                    //TODO
                }
                _launches.Enqueue(launch);
            }
        }

        public Game BuildGame()
        {
            var game = new Game();
            while (_launches.Any())
            {
                var first = _launches.Dequeue();
                var second = _launches.Dequeue();
                Frame frame = Frame.CreateFrame(first, second);
                game.AddFrame(frame);            }


            return game;
        }
    }
}
