using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Model
{
    public class FrameTest
    {
        [Theory]
        [InlineData(1, 5, false, false)]
        [InlineData(2, 7, false, false)]
        public void ValidateFrameCreation(int firstLaunch, int secondLaunch, bool isSpareExpected, bool isStrikeExpected)
        {
            Frame frame = Frame.CreateFrame(firstLaunch, secondLaunch);
            Assert.Equal(isSpareExpected, frame.IsSpare);
            Assert.Equal(isStrikeExpected, frame.IsStrike);
        }
    }
}
