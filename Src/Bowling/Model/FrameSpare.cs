using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Model
{
    internal class FrameSpare : Frame
    {
        protected internal FrameSpare(int firstLaunch, int secondLaunch) : base(firstLaunch, secondLaunch)
        {
            IsSpare = true;
        }

        public override int CalculateFrameScore()
        {
            int baseScore = base.CalculateFrameScore();
            return baseScore + NextFrame.FirstLaunch;
        }
    }
}
