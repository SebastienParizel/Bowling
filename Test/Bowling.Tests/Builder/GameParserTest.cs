using System;
using System.Linq;
using Bowling.Builder;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Builder
{
    public class GameParserTest
    {
        private readonly GameParser _parser;

        public GameParserTest()
        {
            IFrameBuilder builder = new FrameBuilder();
            _parser = new GameParser(builder);
        }

        [Theory]
        [InlineData(2, 7, "1", "1", "3", "2")]
        [InlineData(3, 60, "X", "X", "X")]
        [InlineData(4, 35, "5", "2", "10", "7", "0", "4")]
        [InlineData(10, 10, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1")]
        [InlineData(10, 11, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "1", "1")]
        [InlineData(10, 11, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "00", "01", "1", "1")]
        [InlineData(11, 25, "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "00", "01", "10", "3", "0")]
        public void GivenAValideParameterSet_WhenIParseIt_ThenIGetAValideGame(int expectedFrameCount, int expectedFinalScore, params string[] parameters)
        {
            Game game = _parser.BuildGame(parameters);
            Assert.Equal(expectedFrameCount, game.Count);
            Assert.Equal(expectedFinalScore, game.GetScore());
        }

        [Theory]
        [InlineData("?", "1", "3", "2")]
        [InlineData("0", "1", "3", "a")]
        [InlineData("\t", "1", "3", "2")]
        [InlineData("\0", "1", "3", "2")]
        [InlineData("", "1", "3", "2")]
        public void GivenAnInvalidParameterSet_WhenIParseIt_ThenIGetAnException(params string[] parameters)
        {
            Assert.Throws<BowlingException>(() => _parser.BuildGame(parameters));
        }

        [Theory]
        [InlineData(1, 9, "1", "/", "5", "3")]
        [InlineData(10, 0, "X", "-", "5", "3")]
        [InlineData(10, 0, "X", "5", "3")]
        [InlineData(5, 0, "5", "-", "5", "3")]
        public void GivenSpecialCharacterInParameterSet_WhenIParseIt_ThenIGetAValidGame(int expectedFirstLaunch, int expectedSecondLaunch, params string[] parameters)
        {
            var game = _parser.BuildGame(parameters);
            Assert.Equal(expectedFirstLaunch, game.GetFrames().First().FirstLaunch);
            Assert.Equal(expectedSecondLaunch, game.GetFrames().First().SecondLaunch);
        }
    }
}
