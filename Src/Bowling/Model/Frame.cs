using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Model
{
    public class Frame
    {
        private const int MaxDroppablePins = 10;

        public int FirstLaunch { get; protected set; }
        public int SecondLaunch { get; protected set; }
        public bool IsStrike { get; protected set; } = false;
        public bool IsSpare { get; protected set; } = false;

        public Frame NextFrame { get; private set; } = null;
        public Frame PreviousFrame { get; private set; } = null;

        protected Frame(int firstLaunch, int secondLaunch)
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

        #region static builder
        public static Frame CreateFrame(int firstLaunch, int secondLaunch)
        {
            ValidateFrame(firstLaunch, secondLaunch);
            if (firstLaunch == MaxDroppablePins)
                return new StrikeFrame(firstLaunch, secondLaunch);
            if (firstLaunch + secondLaunch == MaxDroppablePins)
                return new FrameSpare(firstLaunch, secondLaunch);

            return new Frame(firstLaunch, secondLaunch);
        }

        private static void ValidateFrame(int firstLaunch, int secondLaunch)
        {
            ValidateLaunch(firstLaunch);
            ValidateLaunch(secondLaunch);
            ValidatePinNotExceeed(firstLaunch, secondLaunch);

        }

        private static void ValidateLaunch(int launch)
        {
            if (launch < 0 || launch > MaxDroppablePins)
                throw new ArgumentException($"A launch should be between 0 and {MaxDroppablePins} but is [{launch}]");
        }

        private static void ValidatePinNotExceeed(int firstLaunch, int secondLaunch)
        {
            int totalPinDropped = firstLaunch + secondLaunch;
            if (totalPinDropped > MaxDroppablePins)
                throw new ArgumentException($"Maximum ({MaxDroppablePins} pin dropped exceed: {totalPinDropped}");
        }
        #endregion
    }
}
