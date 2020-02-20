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
        [InlineData(2, 8, true, false)]
        [InlineData(3, 7, true, false)]
        [InlineData(0, 10, true, false)]
        [InlineData(10, 0, false, true)]
        public void ValidateFrameCreation(int firstLaunch, int secondLaunch, bool isSpareExpected, bool isStrikeExpected)
        {
            Frame frame = Frame.CreateFrame(firstLaunch, secondLaunch);
            Assert.Equal(isSpareExpected, frame.IsSpare);
            Assert.Equal(isStrikeExpected, frame.IsStrike);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(-1, 7)]
        [InlineData(11, 7)]
        [InlineData(0, 11)]
        [InlineData(0, -1)]
        public void ValidateFrameCreationWithInvalidParameters(int firstLaunch, int secondLaunch)
        {
            var exception = Assert.Throws<ArgumentException>(() => Frame.CreateFrame(firstLaunch, secondLaunch));
            Assert.NotEmpty(exception.Message);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 2, 6)]
        [InlineData(0, 2, 2)]
        public void ValidateFrameScoreCalculation(int firstLaunch, int secondLaunch, int expectedScore)
        {
            var frame = Frame.CreateFrame(firstLaunch, secondLaunch);
            Assert.Equal(expectedScore, frame.CalculateFrameScore());
        }

        [Fact]
        public void ValidateFrameScoreCalculationWithSpare()
        {
            var firstFrame = Frame.CreateFrame(1, 9);
            var secondFrame = Frame.CreateFrame(5, 2);
            firstFrame.SetNextFrame(secondFrame);
            Assert.Equal(7, secondFrame.CalculateFrameScore());
            Assert.Equal(22, firstFrame.CalculateFrameScore());
        }

        [Fact]
        public void ValidateFrameScoreCalculationWithStrike()
        {
            var firstFrame = Frame.CreateFrame(10, 0);
            var secondFrame = Frame.CreateFrame(5, 2);
            firstFrame.SetNextFrame(secondFrame);
            Assert.Equal(7, secondFrame.CalculateFrameScore());
            Assert.Equal(24, firstFrame.CalculateFrameScore());
        }
    }
}
