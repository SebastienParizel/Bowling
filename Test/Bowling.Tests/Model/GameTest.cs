using System;
using Bowling.Builder;
using Bowling.Model;
using Xunit;

namespace Bowling.Tests.Model
{
    public class GameTest
    {
        private readonly Game _game;
        
        public GameTest()
        {
            _game = new Game();
        }

        [Fact]
        public void ValidateGameCreation()
        {
            Assert.Equal(0, _game.Count);
            Assert.Null(_game.LastFrame);
        }

        [Fact]
        public void ValidateFirstFrameAddToGame()
        {
            var frame = new Frame(1, 2);
            _game.AddFrame(frame);
            Assert.Equal(1, _game.Count);
            Assert.Equal(frame, _game.LastFrame);
        }

        [Fact]
        public void ValidateSecondFrameAddToGame()
        {
            var firstFrame = new Frame(1, 2);
            _game.AddFrame(firstFrame);

            var secondFrame = new Frame(3, 4);
            _game.AddFrame(secondFrame);

            Assert.Equal(2, _game.Count);
            Assert.Equal(secondFrame, _game.LastFrame);
        }

        [Fact]
        public void ValidateThirdFrameAddToGame()
        {
            var firstFrame = new Frame(1, 2);
            _game.AddFrame(firstFrame);

            var secondFrame = new Frame(3, 4);
            _game.AddFrame(secondFrame);

            var thirdFrame = new Frame(5, 4);
            _game.AddFrame(thirdFrame);

            Assert.Equal(3, _game.Count);
            Assert.Equal(thirdFrame, _game.LastFrame);
        }

        [Fact]
        public void ValidateCannotPlayMoreThanTenFrame()
        {
            for(int i=0; i<10; i++)
            {
                var frame = new Frame(1, 1);
                _game.AddFrame(frame);
            }
            Assert.Throws<NotSupportedException>(() => _game.AddFrame(new Frame(1, 1)));
        }

        [Fact]
        public void GivenNineFramePlayedWhenThenthFrameIsStrikeThenCanPlayAnExtraLaunch()
        {
            Frame frame;
            var framebuilder = new FrameBuilder();
            for (int i = 0; i < 9; i++)
            {
                frame = framebuilder.CreateFrame(1, 1);
                _game.AddFrame(frame);
            }
            frame = framebuilder.CreateFrame(10, 0);
            _game.AddFrame(frame);

            frame = framebuilder.CreateFrame(9, 0);
            _game.AddFrame(frame);
            Assert.Equal(11, _game.Count);
        }

        [Fact]
        public void ValidateGetFrame()
        {
            Frame[] frames = new Frame[3];
            for (int i = 0; i < 3; i++)
            {
                frames[i] = new Frame(i, i + 1);
                _game.AddFrame(frames[i]);
            }

            int index = 0;
            foreach (var frame in _game.GetFrames())
            {
                Assert.Equal(frames[index], frame);
                index++;
            }
            Assert.Equal(3, index);
        }
    }
}
