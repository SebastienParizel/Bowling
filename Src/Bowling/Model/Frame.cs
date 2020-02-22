using System;

namespace Bowling.Model
{
    public class Frame
    {
        public int FrameNumber { get; private set; } = 1;
        public int FirstLaunch { get; protected set; }
        public int SecondLaunch { get; protected set; }
        public bool IsStrike { get; protected set; } = false;
        public bool IsSpare { get; protected set; } = false;

        public Frame NextFrame { get; private set; } = null;
        public Frame PreviousFrame { get; private set; } = null;

        public Frame(int firstLaunch, int secondLaunch)
        {
            FirstLaunch = firstLaunch;
            SecondLaunch = secondLaunch;
        }

        public void SetNextFrame(Frame frame)
        {
            frame.FrameNumber = FrameNumber + 1;
            NextFrame = frame;
            frame.PreviousFrame = this;
        }

        public int CalculateScore()
        {
            int previousFrameScore = GetPreviousFrameScore();
            int frameScore = CalculateFrameScore();
            return previousFrameScore + frameScore;
        }

        protected virtual int CalculateFrameScore()
        {
            return FirstLaunch + SecondLaunch;
        }

        protected bool HasNextFrame()
        {
            return NextFrame != null;
        }

        private int GetPreviousFrameScore()
        {
            if (PreviousFrame == null)
                return 0;
            return PreviousFrame.CalculateScore();
        }
    }
}
