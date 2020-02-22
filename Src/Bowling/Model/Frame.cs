using System;

namespace Bowling.Model
{
    public class Frame
    {
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
            NextFrame = frame;
            frame.PreviousFrame = this;
        }

        public int CalculateScore()
        {
            int previousFrameScore = GetPreviousFrameScore();
            int frameScore = CalculateFrameScore();
            return previousFrameScore + frameScore;
        }

        public virtual int CalculateFrameScore()
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
