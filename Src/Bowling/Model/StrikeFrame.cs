using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Model
{
    internal class StrikeFrame : Frame
    {
        protected internal StrikeFrame(int firstLaunch, int secondLaunch) : base(firstLaunch, secondLaunch)
        {
            IsStrike = true;
        }
    }
}
