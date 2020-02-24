using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Model;

namespace Bowling.Builder
{
    public class FrameBuilder : IFrameBuilder
    {
        private const int MaxDroppablePins = 10;

        public Frame CreateFrame(int firstLaunch, int secondLaunch)
        {
            ValidateFrame(firstLaunch, secondLaunch);
            if (firstLaunch == MaxDroppablePins)
                return new StrikeFrame(firstLaunch, secondLaunch);
            if (firstLaunch + secondLaunch == MaxDroppablePins)
                return new SpareFrame(firstLaunch, secondLaunch);

            return new Frame(firstLaunch, secondLaunch);
        }

        public Frame CreateSpare(int firstLaunch)
        {
            ValidateLaunch(firstLaunch);
            var secondLaunch = GetSparedLaunch(firstLaunch);
            return new SpareFrame(firstLaunch, secondLaunch);

        }

        public Frame CreateStrike()
        {
            return new StrikeFrame(10, 0);
        }

        private int GetSparedLaunch(int firstLaunch)
        {
            return MaxDroppablePins - firstLaunch;
        }

        private void ValidateFrame(int firstLaunch, int secondLaunch)
        {
            ValidateLaunch(firstLaunch);
            ValidateLaunch(secondLaunch);
            ValidatePinNotExceeed(firstLaunch, secondLaunch);

        }

        private void ValidateLaunch(int launch)
        {
            if (launch < 0 || launch > MaxDroppablePins)
                throw new BowlingException($"A launch should be between 0 and {MaxDroppablePins} but is [{launch}]");
        }

        private void ValidatePinNotExceeed(int firstLaunch, int secondLaunch)
        {
            int totalPinDropped = firstLaunch + secondLaunch;
            if (totalPinDropped > MaxDroppablePins)
                throw new BowlingException($"Maximum ({MaxDroppablePins} pin dropped exceed: {totalPinDropped}");
        }
    }
}
