using System;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Model
{
    public class GameTest
    {
        [Fact]
        public void ValidateGameCreation()
        {
            var game = new Game();
            Assert.Equal(0, game.Count);
            Assert.Null(game.FirstFrame);
            Assert.Null(game.LastFrame);
        }

        [Fact]
        public void ValidateFirstFrameAddToGame()
        {
            var game = new Game();
            var frame = Frame.CreateFrame(1, 2);
            game.AddFrame(frame);
            Assert.Equal(1, game.Count);
            Assert.Equal(frame, game.FirstFrame);
            Assert.Equal(frame, game.LastFrame);
        }

        [Fact]
        public void ValidateSecondFrameAddToGame()
        {
            var game = new Game();
            var firstFrame = Frame.CreateFrame(1, 2);
            game.AddFrame(firstFrame);

            var secondFrame = Frame.CreateFrame(3, 4);
            game.AddFrame(secondFrame);

            Assert.Equal(2, game.Count);
            Assert.Equal(firstFrame, game.FirstFrame);
            Assert.Equal(secondFrame, game.LastFrame);
        }

        [Fact]
        public void ValidateThirdFrameAddToGame()
        {
            var game = new Game();
            var firstFrame = Frame.CreateFrame(1, 2);
            game.AddFrame(firstFrame);

            var secondFrame = Frame.CreateFrame(3, 4);
            game.AddFrame(secondFrame);

            var thirdFrame = Frame.CreateFrame(5, 4);
            game.AddFrame(thirdFrame);

            Assert.Equal(3, game.Count);
            Assert.Equal(firstFrame, game.FirstFrame);
            Assert.Equal(thirdFrame, game.LastFrame);
        }

        [Fact]
        public void ValidateCannotPlayMoreThanTenFrame()
        {
            var game = new Game();
            for(int i=0; i<10; i++)
            {
                var frame = Frame.CreateFrame(1, 1);
                game.AddFrame(frame);
            }
            Assert.Throws<NotSupportedException>(() => game.AddFrame(Frame.CreateFrame(1, 1)));
        }

        [Fact]
        public void GivenNineFramePlayedWhenThenthFrameIsStrikeThenCanPlayAnExtraLaunch()
        {
            var game = new Game();
            Frame frame;
            for (int i = 0; i < 9; i++)
            {
                frame = Frame.CreateFrame(1, 1);
                game.AddFrame(frame);
            }
            frame = Frame.CreateFrame(10, 0);
            game.AddFrame(frame);

            frame = Frame.CreateFrame(9, 0);
            game.AddFrame(frame);
            Assert.Equal(11, game.Count);
        }

        [Fact]
        public void ValidateGetFrame()
        {
            var game = new Game();
            Frame[] frames = new Frame[3];
            for (int i = 0; i < 3; i++)
            {
                frames[i] = Frame.CreateFrame(i, i + 1);
                game.AddFrame(frames[i]);
            }

            int index = 0;
            foreach (var frame in game.GetFrames())
            {
                Assert.Equal(frames[index], frame);
                index++;
            }
            Assert.Equal(3, index);
        }
    }
}
