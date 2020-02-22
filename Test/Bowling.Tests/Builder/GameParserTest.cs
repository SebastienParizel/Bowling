using Bowling.Builder;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Builder
{
    public class GameParserTest
    {
        [Theory]
        [InlineData(2, 7, "1", "1", "3", "2")]
        public void GivenAValideParameterSet_WhenIPaseIt_ThenIGetAValideGame(int expectedFrameCount, int expectedFinalScore, params string[] parameters)
        {
            var builder = new GameParser(parameters);
            Game game = builder.BuildGame();
            Assert.Equal(expectedFrameCount, game.Count);
            Assert.Equal(expectedFinalScore, game.GetScore());
        }
    }
}
