using System;
using Bowling.Builder;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Builder
{
    public class GameParserTest
    {
        [Theory]
        [InlineData(2, 7, "1", "1", "3", "2")]
        [InlineData(10, 10, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1")]
        [InlineData(10, 11, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "1", "1")]
        [InlineData(10, 11, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "00", "01", "1", "1")]
        [InlineData(11, 25, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "00", "01", "10", "0", "3", "0")]
        public void GivenAValideParameterSet_WhenIParseIt_ThenIGetAValideGame(int expectedFrameCount, int expectedFinalScore, params string[] parameters)
        {
            var builder = new GameParser(parameters);
            Game game = builder.BuildGame();
            Assert.Equal(expectedFrameCount, game.Count);
            Assert.Equal(expectedFinalScore, game.GetScore());
        }

        [Theory]
        [InlineData("?", "1", "3", "2")]
        [InlineData("0", "1", "3", "a")]
        [InlineData("0", "1", "3")]
        [InlineData("\t", "1", "3", "2")]
        [InlineData("\0", "1", "3", "2")]
        [InlineData("", "1", "3", "2")]
        public void GivenAnInvalidParameterSet_WhenIParseIt_ThenIGetAnException(params string[] parameters)
        {
            Assert.Throws<ArgumentException>(() => new GameParser(parameters));
        }
    }
}
