using System;
using System.Collections.Generic;

namespace Bowling.Model
{
    public class Game
    {
        private const int MaxFrameAllowed = 10;

        public Frame FirstFrame { get; private set; } = null;

        public Frame LastFrame { get; private set; } = null;

        private List<Frame> _frames = new List<Frame>();

        public int Count { get; private set; } = 0;

        public void AddFrame(Frame frame)
        {
            if (HasNoFrame())
            {
                FirstFrame = frame;
                LastFrame = frame;
                _frames.Add(frame);
                Count = 1;
            }
            else
            {
                if (!CanAddFrame(frame))
                    throw new NotSupportedException();
                LastFrame.SetNextFrame(frame);
                LastFrame = frame;
                _frames.Add(frame);
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

        public IEnumerable<Frame> GetFrames()
        {
            return _frames;
        }
    }
}
