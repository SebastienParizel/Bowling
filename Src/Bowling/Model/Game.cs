using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Model
{
    public class Game
    {
        private const int MaxFrameAllowed = 10;
        public Frame LastFrame { get; private set; } = null;

        private List<Frame> _frames = new List<Frame>();

        public int Count => _frames.Count();

        public void AddFrame(Frame frame)
        {
            if (HasNoFrame())
            {
                LastFrame = frame;
                AddLinkedFrame(frame);
            }
            else
            {
                if (!CanAddFrame(frame))
                    throw new BowlingException("To much frame in this party");

                LastFrame.SetNextFrame(frame);
                AddLinkedFrame(frame);
            }
        }

        private void AddLinkedFrame(Frame frame)
        {
            LastFrame = frame;
            _frames.Add(frame);
        }

        public int GetScore()
        {
            return LastFrame.CalculateScore();
        }

        private bool HasNoFrame()
        {
            return Count == 0;
        }

        private bool CanAddFrame(Frame frame)
        {
            if (Count < MaxFrameAllowed)
                return true;

            if (Count >= MaxFrameAllowed + 1)
                return false;

            return LastFrame.IsStrike && frame.SecondLaunch == 0;
        }

        public IEnumerable<Frame> GetFrames()
        {
            return _frames;
        }
    }
}
