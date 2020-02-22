using System;

namespace Bowling.Model
{
    public class Game
    {
        private const int MaxFrameAllowed = 10;

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
                if (!CanAddFrame(frame))
                    throw new NotSupportedException();
                LastFrame.SetNextFrame(frame);
                LastFrame = frame;
                Count++;
            }
        }

        private bool HasNoFrame()
        {
            return Count == 0;
        }

        private bool CanAddFrame(Frame frame)
        {
            if (Count < MaxFrameAllowed)
                return true;

            return LastFrame.IsStrike && frame.SecondLaunch == 0;
        }
    }
}
