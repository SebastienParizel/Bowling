using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Model
{
    public class Game
    {
        public Frame FirstFrame { get; private set; } = null;

        public Frame LastFrame { get; private set; } = null;

        public int Count { get; private set; } = 0;

        public void AddFrame(Frame frame)
        {
            if (HasNoFrame())
            {
                FirstFrame = frame;
                LastFrame = frame;
                Count = 1;
            }
            else
            {
                LastFrame.SetNextFrame(frame);
                LastFrame = frame;
                Count++;
            }
        }

        private bool HasNoFrame()
        {
            return Count == 0;
        }
    }
}
