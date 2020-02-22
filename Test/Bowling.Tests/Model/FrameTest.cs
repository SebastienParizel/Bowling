using System;
using Bowling.Builder;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Model
{
    public class FrameTest
    {
        private readonly IFrameBuilder _frameBuilder;

        public FrameTest()
        {
            _frameBuilder = new FrameBuilder();
        }

        [Theory]
        [InlineData(1, 5, false, false)]
        [InlineData(2, 7, false, false)]
        [InlineData(2, 8, true, false)]
        [InlineData(3, 7, true, false)]
        [InlineData(0, 10, true, false)]
        [InlineData(10, 0, false, true)]
        public void ValidateFrameCreation(int firstLaunch, int secondLaunch, bool isSpareExpected, bool isStrikeExpected)
        {
            Frame frame = _frameBuilder.CreateFrame(firstLaunch, secondLaunch);
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
            var exception = Assert.Throws<ArgumentException>(() => _frameBuilder.CreateFrame(firstLaunch, secondLaunch));
            Assert.NotEmpty(exception.Message);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 2, 6)]
        [InlineData(0, 2, 2)]
        public void ValidateFrameScoreCalculation(int firstLaunch, int secondLaunch, int expectedScore)
        {
            var frame = _frameBuilder.CreateFrame(firstLaunch, secondLaunch);
            Assert.Equal(expectedScore, frame.CalculateScore());
        }

        [Fact]
        public void ValidateFrameScoreCalculationWithSpare()
        {
            var firstFrame = _frameBuilder.CreateFrame(1, 9);//10 + 5 -> 15
            var secondFrame = _frameBuilder.CreateFrame(5, 2);//7 + 15 => 
            firstFrame.SetNextFrame(secondFrame);
            Assert.Equal(15, firstFrame.CalculateScore());
            Assert.Equal(22, secondFrame.CalculateScore());
        }

        [Fact]
        public void ValidateFrameScoreCalculationWithSpareAsLastLaunch()
        {
            var firstFrame = _frameBuilder.CreateFrame(1, 9);
            Assert.Equal(10, firstFrame.CalculateScore());
        }

        [Fact]
        public void ValidateFrameScoreCalculationWithStrike()
        {
            var firstFrame = _frameBuilder.CreateFrame(10, 0);//10 + 5 + 2 -> 17
            var secondFrame = _frameBuilder.CreateFrame(5, 2); // 7 + 17 -> 24
            firstFrame.SetNextFrame(secondFrame);
            Assert.Equal(17, firstFrame.CalculateScore());
            Assert.Equal(24, secondFrame.CalculateScore());
        }

        [Fact]
        public void ValidateFrameScoreCalculationWithStrikeAsLastLaunch()
        {
            var firstFrame = _frameBuilder.CreateFrame(10, 0);
            Assert.Equal(10, firstFrame.CalculateScore());
        }
    }
}
